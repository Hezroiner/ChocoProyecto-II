﻿using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _svUser;

        public UserController(IUser svUser)
        {
            _svUser = svUser;
        }

        //Get All
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _svUser.GetAll();
        }

        //GetById
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _svUser.GetById(id);
        }

        //Post
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _svUser.AddUser(user);
        }
    }
}