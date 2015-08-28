using Newtonsoft.Json;
using NoteKeeper.Core;
using NoteKeeper.Core.Abstracts;
using NoteKeeper.Core.Models;
using NoteKeeper.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace NoteKeeper.Website.Controllers.API
{
    [RoutePrefix("api/v1/Notes")]
    [Route("{action=Get}")]
    public class NoteController : ApiController
    {
        INoteController noteController = NoteKeeperFactory.CreateNoteController();

        [HttpGet]
        [Route("")]
        public APIResult<List<Note>> GetAll()
        {
            APIResult<List<Note>> result = new APIResult<List<Note>>();
            result.Data = noteController.GetAll();
            return result;
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public Note FindById(int id)
        {
            return NoteKeeperFactory.CreateNoteController().Get(id);
        }

        [HttpPost]
        [Route("add")]
        public bool Add(Note note)
        {
            noteController.Add(note);
            return true;
        }

        [HttpPost]
        [Route("update")]
        public bool Update(Note note)
        {
            noteController.Update(note);
            return true;
        }

        [HttpPost]
        [Route("remove/{id:int}")]
        public bool Remove(int id)
        {
            noteController.Delete(id);
            return true;
        }

        #region V2

        [HttpGet]
        [Route("~/api/v2/notes/{id:int}")]
        public Note Get(int id)
        {
            return this.noteController.Get(id);
        }

        [HttpGet]
        [Route("~/api/v2/notes")]
        public IEnumerable<Note> Get()
        {
            return this.noteController.GetAll();
        }

        #region Post Requests
        [HttpPost]
        [Route("~/api/v2/notes/add")]
        public HttpResponseMessage AddV2(Note note)
        {
            try
            {
                this.noteController.Add(note);
                var response = this.Request.CreateResponse<Note>(HttpStatusCode.Created, note);
                //prepare link for the newly created resource.to access it, view it on request header location
                string uri = Url.Content("/api/v2/notes/") + note.Id;
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost]
        [Route("~/api/v2/notes/update")]
        public HttpResponseMessage UpdateV2(Note note)
        {
            try
            {
                this.noteController.Update(note);
                var response = this.Request.CreateResponse<Note>(HttpStatusCode.NoContent, note);
                //prepare link for the newly created resource.to access it, view it on request header location
                string uri = Url.Content("/api/v2/notes/") + note.Id;
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost]
        [Route("~/api/v2/notes/delete/{id:int}")]
        public HttpResponseMessage DeleteV2(int id)
        {
            try
            {
                this.noteController.Delete(id);
                var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
                string uri = Url.Content("/api/v2/notes");
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        } 
        #endregion

        #endregion

        #region Exception Handling
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    //If the exeption is already handled we do nothing
        //    if (filterContext.ExceptionHandled)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //Determine the return type of the action
        //        string actionName = filterContext.RouteData.Values["action"].ToString();
        //        Type controllerType = filterContext.Controller.GetType();
        //        var method = controllerType.GetMethod(actionName);
        //        var returnType = method.ReturnType;

        //        //If the action that generated the exception returns JSON
        //        if (returnType.Equals(typeof(JsonResult)))
        //        {
        //            filterContext.Result = new JsonResult()
        //            {
        //                Data = "Return data here"
        //            };
        //        }

        //        //If the action that generated the exception returns a view
        //        if (returnType.Equals(typeof(ActionResult))
        //            || (returnType).IsSubclassOf(typeof(ActionResult)))
        //        {
        //            filterContext.Result = new ViewResult()
        //            {
        //                ViewName = "URL to the errror page"
        //            };
        //        }
        //    }

        //    //Make sure that we mark the exception as handled
        //    filterContext.ExceptionHandled = true;
        //}
        #endregion
    }
}
