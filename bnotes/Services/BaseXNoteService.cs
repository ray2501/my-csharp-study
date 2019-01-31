using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using bnotes.Data;
using BaseXClient;

namespace bnotes.Services
{
    public class BaseXNoteService : INoteService
    {
        private Serializer ser;
        private readonly ILogger<BaseXNoteService> _logger;
        private string host;
        private int port;
        private string user;
        private string pass;

        public BaseXNoteService(string host, int port, string user, string pass,
            ILogger<BaseXNoteService> logger)
        {
            _logger = logger;
            this.host = host;
            this.port = port;
            this.user = user;
            this.pass = pass;

            ser = new Serializer();
        }

        public Session Connection
        {
            get
            {
                try
                {
                    return new Session(host, port, user, pass);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"BaseXNoteService Connection: {ex.Message}");
                    throw;
                }
            }
        }

        public IEnumerable<Note> GetAll()
        {
            List<Note> results = new List<Note>();
            Session session = null;

            try
            {
                session = Connection;

                string queryString = $"let $doc := db:open('danilo', 'Notes') " +
                                     $"return $doc";
                string xmlOutputData = session.Execute($"xquery {queryString}");
                results = ser.Deserialize<List<Note>>(xmlOutputData);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"BaseXNoteService GetAll: {ex.Message}");
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            return results;
        }

        public Note GetNoteById(Guid id)
        {
            Session session = null;
            Note note = null;

            try
            {
                session = Connection;

                string queryString = $"for $doc in db:open('danilo', 'Notes')//Note " +
                                     $"where $doc/Id[text() = '{id}'] " +
                                     $"return $doc";
                string xmlOutputData = session.Execute($"xquery {queryString}");
                XElement element = XElement.Parse(xmlOutputData);
                note = ser.FromXElement<Note>(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"BaseXNoteService GetNoteById: {ex.Message}");
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            return note;
        }

        public void AddNote(Note note)
        {
            Session session = null;

            // We need generate Guid here
            note.Id = System.Guid.NewGuid();
            note.Created = DateTime.Now;

            try
            {
                session = Connection;
                XElement element = ser.ToXElement<Note>(note);
                string newNode = element.ToString();

                string insertString = $"insert node {newNode} as last into db:open('danilo', 'Notes')//ArrayOfNote";
                session.Execute($"xquery {insertString}");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"BaseXNoteService AddNote: {ex.Message}");
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }
        }

        public void UpdateNote(Note note)
        {
            Session session = null;

            try
            {
                session = Connection;
                XElement element = ser.ToXElement<Note>(note);
                string newNode = element.ToString();
                string queryString = $"for $doc in db:open('danilo', 'Notes')//Note " +
                                      $"where $doc/Id[text() = '{note.Id}'] " +
                                     $"return replace node $doc with {newNode}";
                session.Execute($"xquery {queryString}");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"BaseXNoteService UpdateNote: {ex.Message}");
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }
        }

        public void DeleteNoteById(Guid id)
        {
            Session session = null;

            try
            {
                session = Connection;

                string queryString = $"for $doc in db:open('danilo', 'Notes')//Note " +
                                     $"where $doc/Id[text() = '{id}'] " +
                                     $"return delete node $doc";
                session.Execute($"xquery {queryString}");
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"BaseXNoteService DeleteNoteById: {ex.Message}");
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }
        }
    }
}
