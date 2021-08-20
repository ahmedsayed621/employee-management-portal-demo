using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configration;

        public DepartmentController(IConfiguration configuration)
        {
            _configration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @" select DepartmentId , DepartmentName from Department";
            DataTable table = new DataTable();

            string SqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();

                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }


            }
            return new JsonResult(table);

        }

        [HttpPost]

        public JsonResult Post(Department dep)
        {
            string query = @" insert into Department values('" + dep.DepartmentName + @"')";
            DataTable table = new DataTable();

            string SqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();

                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Success");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @" update Department set
            DepartmentName = '" + dep.DepartmentName + @"'where DepartmentId = " + dep.DepartmentId + @"";
            DataTable table = new DataTable();

            string SqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();

                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("updated Success");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @" delete from Department where DepartmentId = " + id + @"";
            DataTable table = new DataTable();

            string SqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();

                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("deleted Success");
        }


    }
}

