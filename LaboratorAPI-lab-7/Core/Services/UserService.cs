using Core.Dtos;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Mapping;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private AuthorizationService authService { get; set; }

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var hashedPassword = authService.HashPassword(registerData.Password);

            var user = new User
            {
                Username = registerData.Username,
                Email = registerData.Email,
                Password = hashedPassword,
                RoleId = registerData.RoleId
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }

        public string Validate(LoginDto payload)
        {
            var user = unitOfWork.Users.GetByUsername(payload.Username);

            var passwordFine = authService.VerifyHashedPassword(user.Password, payload.Password);

            if (passwordFine)
            {

                var role = unitOfWork.Roles.GetById(user.RoleId);

                return authService.GetToken(user, role.Name);

                //var role = GetRole(user);

                //return authService.GetToken(user, role);
            }
            else
            {
                return null;
            }

        }

        public string GetRole(User user)
        {
            if (user.RoleId == 1)
            {
                return "Student";
            }
            else
            {
                return "Teacher";
            }
        }

        public List<User> GetAll()
        {
            var results = unitOfWork.Users.GetAll();

            return results;
        }

        public UserDto GetById(int userId)
        {
            var user = unitOfWork.Users.GetById(userId);

            var result = user.ToUserDto();

            return result;
        }

        public List<DataLayer.Dtos.GradeDto> GetGradesByStudent(int userId)
        {
            var student = unitOfWork.Students.GetByUserId(userId);

            var grades = student.Grades;

            List<DataLayer.Dtos.GradeDto> result = student.Grades.ToGradeDtos();

            return result;
        }

        public List<GradesByStudent> GetGradesForTeacher()
        {
            var allGradesByStudent = new List<GradesByStudent>();

            var students = unitOfWork.Students.GetStudentsWithGrades();

            foreach (var student in students)
            {
                var result = new GradesByStudent(student);
                allGradesByStudent.Add(result);
            }

            return allGradesByStudent;
        }
    }
}
