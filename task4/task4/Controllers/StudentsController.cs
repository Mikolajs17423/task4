using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task4.Models;
using task4.Services;

namespace task4.Controllers
{


      

        [ApiController]
        [Route("api/students")]
        public class StudentsController : ControllerBase
        {
        private const string ConString = "Data Source=POL1-1028444LT;Initial Catalog=baza;Integrated Security=True;Pooling=False";



            [HttpGet]
            public IActionResult GetStudents()
            {

                var result = new List<Student>();

                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select * from students";

                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var st = new Student();
                    st.Id = dr["Id"].ToString();
                    st.sName = dr["sName"].ToString();
                    
                     
                     result.Add(st);
                    }

                    return Ok(result);
                }
            }

            [HttpGet("{indexNumber}")]
            public IActionResult GetStudent(string indexNumber)
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select * from students where Id=@index";

                    SqlParameter par1 = new SqlParameter();
                    par1.ParameterName = "index";
                    par1.Value = indexNumber;

                    com.Parameters.Add(par1);
                    //com.Parameters.AddWithValue("index", indexNumber);

                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        var st = new Student();
                    st.Id = dr["Id"].ToString();
                    st.sName = dr["sName"].ToString();
                    return Ok(st);
                    }

                    return Ok();
                }
            }

            [HttpPost]
            public IActionResult CreateStudent([FromServices] IStudentsDb service, Student newStudent)
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "insert into students(Id, sName) values (@par1, @par2)";

                    com.Parameters.AddWithValue("par1", newStudent.Id);
                    com.Parameters.AddWithValue("par2", newStudent.sName);

                    con.Open();
                    int affectedRows = com.ExecuteNonQuery();
                }

                return Ok();
            }

            [HttpGet("procedure")]
            public IActionResult GetStudents2()
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "TestProc3";
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("sName", "John");

                    var dr = com.ExecuteReader();
                     if (dr.Read())
                {
                    var student = new Student();

                    student.Id = dr["Id"].ToString();
                    student.sName = dr["sName"].ToString();
                 
                    return Ok(student);
                }
                }

                return Ok();
            }

            [HttpGet("procedure2")]
            public IActionResult GetStudents3()
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "insert ...";
                    com.Connection = con;

                    con.Open();

                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        com.ExecuteNonQuery(); //insert

                        com.CommandText = "update sth...";
                        com.ExecuteNonQuery();
                        tran.Commit();
                    }
                    catch (Exception exc)


                    {
                        tran.Rollback();
                    }

                }

                return Ok();
            }
        }
    }
