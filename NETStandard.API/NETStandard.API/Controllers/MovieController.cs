﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETStandard.Repository;
using NETStandard.Entities;
using NETStandard.UnitOfWork;
using Microsoft.Extensions.Options;

namespace NETStandard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private string _connectionString;
        public MovieController(IOptions<ConnectionConfig> connectionConfig)
        {
            var connection = connectionConfig.Value;
            this._connectionString = connection.DbConn;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (DapperUnitOfWork uow = new DapperUnitOfWork(_connectionString))
            {
                var query = uow.MovieRepository.GetAll();
                return Ok(query.ToList());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Movie item;
            using (DapperUnitOfWork uow = new DapperUnitOfWork(_connectionString))
            {
                item = uow.MovieRepository.GetById(id);
            }
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST: api/Movie
        [HttpPost]
        public IActionResult Create([FromBody]Movie movie)
        {
            int x;
            try
            {
                if (movie == null)
                    return BadRequest();
                using (DapperUnitOfWork uow = new DapperUnitOfWork(_connectionString))
                {
                    x = uow.MovieRepository.Add(movie);
                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(x);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Movie item)
        {
            try
            {
                if (item == null || item.Id != id)
                    return BadRequest();
                using (DapperUnitOfWork uow = new DapperUnitOfWork(_connectionString))
                {
                    if (uow.MovieRepository.GetById(item.Id) == null)
                        return NotFound();
                    int x = uow.MovieRepository.Update(item);
                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int x;
                using (DapperUnitOfWork uow = new DapperUnitOfWork(_connectionString))
                {
                    if (uow.MovieRepository.GetById(id) == null)
                        return NotFound();
                    x = uow.MovieRepository.Delete(id);
                    uow.Commit();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return new NoContentResult();
        }
    }
}
