using System;
using System.Collections.Generic;
using System.Text;
using Nancy;
using Nancy.Helpers;
using Nancy.Security;
using NancyApplication.Data;
using NancyApplication.Services;
using Newtonsoft.Json;

namespace NancyApplication
{
    public class HomeModule : NancyModule
    {
        private readonly INoteService _noteService;

        public HomeModule(INoteService service)
        {
            _noteService = service;

            this.RequiresAuthentication();

            Get("/", async _ =>
            {
                IEnumerable<Note> Notes = await _noteService.GetAllAsync();
                string myJsonString = JsonConvert.SerializeObject(Notes, Formatting.Indented);
                var jsonBytes = Encoding.UTF8.GetBytes(myJsonString);

                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Get("/get/{id}", action: async args =>
            {
                Guid guid;
                try
                {
                    guid = Guid.Parse(args.id);
                }
                catch (FormatException)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                Note note = await _noteService.GetNoteByIdAsync(guid);
                if (note != null)
                {
                    string myJsonString = JsonConvert.SerializeObject(note, Formatting.Indented);
                    var jsonBytes = Encoding.UTF8.GetBytes(myJsonString);

                    return new Response
                    {
                        ContentType = "application/json",
                        StatusCode = HttpStatusCode.OK,
                        Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                    };
                }
                else
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
            });

            Post("/add/{title}", action: async args =>
            {
                String title = HttpUtility.UrlDecode(args.title);

                var body = this.Request.Body;
                int length = (int)body.Length;
                byte[] data = new byte[length];
                body.Read(data, 0, length);
                string byteBody = System.Text.Encoding.Default.GetString(data);
                string decodeBody = HttpUtility.UrlDecode(byteBody);

                Note note = new Note { Title = title, Body = decodeBody };

                await _noteService.AddNoteAsync(note);
                return new Response
                {
                    StatusCode = HttpStatusCode.Created
                };
            });

            Put("/update/{id}/{title}", action: async args =>
            {
                Guid guid;
                try
                {
                    guid = Guid.Parse(args.id);
                }
                catch (FormatException)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                Note note = await _noteService.GetNoteByIdAsync(guid);
                if (note == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                String title = HttpUtility.UrlDecode(args.title);

                var body = this.Request.Body;
                int length = (int)body.Length;
                byte[] data = new byte[length];
                body.Read(data, 0, length);
                string byteBody = System.Text.Encoding.Default.GetString(data);
                string decodeBody = HttpUtility.UrlDecode(byteBody);

                note.Title = title;
                note.Body = decodeBody;
                await _noteService.UpdateNoteAsync(note);

                return new Response
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            });

            Delete("/remove/{id}", action: async args =>
            {
                Guid guid;
                try
                {
                    guid = Guid.Parse(args.id);
                }
                catch (FormatException)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                Note note = await _noteService.GetNoteByIdAsync(guid);
                if (note == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                await _noteService.DeleteNoteByIdAsync(guid);

                return new Response
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            });

        }
    }
}
