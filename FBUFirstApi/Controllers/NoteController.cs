using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using FBUFirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBUFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        NotRepository _repo;
        public NoteController(NotRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<NoteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NoteController>/5
        [HttpGet("{id}")]
        public NoteDto Get(int id)
        {
            List<Not> notlar = _repo.ListByOgrId(id);
           
            NoteDto result = new NoteDto();
            result.Success = true;
            result.Notlar =new List<OneNoteDto>();

            foreach(var item in notlar)
            {
                OneNoteDto nt = new OneNoteDto();
                nt.Id = item.Id;
                nt.Text = item.Text;
                result.Notlar.Add(nt);
            }
            result.OgrId = id;
            return result;
        }

        // POST api/<NoteController>
        [HttpPost]
        public bool Post([FromForm]int ogrId,[FromForm] string noteText,[FromForm] int notId)
        {
            Not not = new Not();
            not.OgrId = ogrId;
            not.Text = noteText;
            not.Id = notId;
            _repo.AddOrUpdate(not);
            return true;
        }

        // PUT api/<NoteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
