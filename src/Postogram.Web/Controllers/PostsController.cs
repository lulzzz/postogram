using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Postogram.Web.Controllers
{
    [Route("api/[controller]")]
    public class PostsController
    {
        [HttpGet]
        public IEnumerable<Content> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IEnumerable<Content> Get(object id)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = nameof(Accept))]
        public void Accept(object id)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = nameof(Reject))]
        public void Reject(object id)
        {
            throw new NotImplementedException();
        }
    }
}
