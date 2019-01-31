#!/usr/bin/tclsh

package require BaseXClient

set client [Session new localhost 1984 admin admin]
if {[catch {$client connect}]!=0} {
    puts "Connect fail."
    exit
}

# Create or Open a database
$client execute "check danilo"

set result [$client execute "xquery db:exists('danilo','Notes')"]
if {[string compare [lindex $result 0] "false"]==0} {
     # Initial our document
     set queryString {let $doc := '<ArrayOfNote />' 
     return db:add('danilo', $doc, 'Notes')};
     
     set result [$client execute "xquery $queryString"]
     puts [lindex $result 1]
} else {
     puts "document already exists!!!"
}

$client close
