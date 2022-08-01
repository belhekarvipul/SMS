using Newtonsoft.Json;
using SMS.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMS.WebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        public string studentsData = Helper.GetDataFilePhysicalPath(Constants.StudentsData);
        List<Student> students = new List<Student>();

        StudentsController()
        {
            var jsonData = File.ReadAllText(studentsData);
            students = JsonConvert.DeserializeObject<List<Student>>(jsonData);
        }

        [HttpGet]
        public HttpResponseMessage GetStudents()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetStudents(int id)
        {
            try
            {
                Student student = students.FirstOrDefault(x => x.StudentId == id);
                if (student != null)
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Requested student with Id = " + id + " not found in the system!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddStudent(Student student)
        {
            try
            {
                students.Add(student);
                var jsonData = JsonConvert.SerializeObject(students);
                File.WriteAllText(studentsData, jsonData);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateStudent(Student student)
        {
            try
            {
                int studentIndex = students.IndexOf(students.FirstOrDefault(s => s.StudentId == student.StudentId));
                if (studentIndex >= 0)
                {
                    students[studentIndex] = student;
                    var jsonData = JsonConvert.SerializeObject(students);
                    File.WriteAllText(studentsData, jsonData);

                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Requested student with Id = " + student.StudentId + " not found in the system!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteStudent(int id)
        {
            try
            {
                int studentIndex = students.IndexOf(students.FirstOrDefault(s => s.StudentId == id));
                if (studentIndex >= 0)
                {
                    students.RemoveAt(studentIndex);
                    var jsonData = JsonConvert.SerializeObject(students);
                    File.WriteAllText(studentsData, jsonData);

                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Requested student with Id = " + id + " not found in the system!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}