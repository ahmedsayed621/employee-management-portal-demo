using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;



namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment environment )
        {
            _configration = configuration;
            _env = environment;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @" select EmployeeId,EmployeeName
                           Department ,convert(varchar(10),DateOfJoining,120) as DateOfJoining
                           ,ProfileFileName
                           
                           from Employee";
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

        public JsonResult Post(Employee Emp)
        {
            string query = @" insert into Employee
                             (EmployeeName , Department , DateOfJoining , ProfileFileName)values
                           ('" + Emp.EmployeeName + @"'
                              ,'" + Emp.Department + @"'
                              ,'"+ Emp.DateOfJoining + @"'
                              ,'"+ Emp.ProfileFileName + @"'
                           )";
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
        public JsonResult Put(Employee Emp)
        {
            string query = @" update Employee set
            EmployeeName = '" + Emp.EmployeeName + @"',
            Department = '" + Emp.Department + @"',
            DateOfJoining = '" + Emp.DateOfJoining + @"'

            where EmployeeId = " + Emp.EmployeeId + @"";
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
            string query = @" delete from Employee where EmployeeId = " + id + @"";
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

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var PostedFile = httpRequest.Files[0];
                string filename = PostedFile.FileName;

                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                using(var stream = new FileStream(physicalPath,FileMode.Create))
                {
                    PostedFile.CopyTo(stream);
                }
                return new JsonResult(filename);

            }
            catch (Exception)
            {

                return new JsonResult("Bebo.png");
            }

        }

        [Route("GetAllDepartmentNames")]

        public JsonResult GetAllDepartmentNames()
        {
            string query = @"select DepartmentName 
                           
                           from Department";
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
    }
}
