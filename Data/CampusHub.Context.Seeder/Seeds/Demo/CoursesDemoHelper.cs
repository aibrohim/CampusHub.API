namespace CampusHub.Context.Seeder;

using CampusHub.Context.Entities;

public class CoursesDemoHelper
{
    public IEnumerable<Course> SampleCourses = new List<Course>
    {
        new Course()
        {
            Name = "BSc (Hons) in Business Information Systems",
            Uid = Guid.NewGuid(),
            Modules = new List<Module>()
            {
                new Module() { Name = "Fundamentals of Programming", Uid = Guid.NewGuid() },
                new Module() { Name = "Introduction to Statistics and Data Science", Uid = Guid.NewGuid() },
                new Module() { Name = "Introduction to Management and Organisational Behaviour", Uid = Guid.NewGuid() },
                new Module() { Name = "Web Technology", Uid = Guid.NewGuid() },
                new Module() { Name = "Computer Science Fundamentals", Uid = Guid.NewGuid() },
                new Module() { Name = "Mathematics for computing", Uid = Guid.NewGuid() },
                new Module() { Name = "Graphic design", Uid = Guid.NewGuid() },
                new Module() { Name = "Business Information Systems Project", Uid = Guid.NewGuid() },
                new Module() { Name = "Developing Digital Enterprise", Uid = Guid.NewGuid() },
                new Module() { Name = "Software Quality, Performance and Testing", Uid = Guid.NewGuid() },
                new Module() { Name = "Concurrent Programming", Uid = Guid.NewGuid() },
                new Module() { Name = "Distributed Systems and Cloud Computing", Uid = Guid.NewGuid() },
                new Module() { Name = "ERP Design and Implementation", Uid = Guid.NewGuid() },
                new Module() { Name = "Internet of Things", Uid = Guid.NewGuid() },
                new Module() { Name = "CGI and Its Implementation", Uid = Guid.NewGuid() },
                new Module() { Name = "Information Security", Uid = Guid.NewGuid() },
                new Module() { Name = "Strategy in a Complex World", Uid = Guid.NewGuid() },
                new Module() { Name = "Project Management", Uid = Guid.NewGuid() },
                new Module() { Name = "Digital Marketing", Uid = Guid.NewGuid() },
                new Module() { Name = "Machine learning and Data Analytics", Uid = Guid.NewGuid() }
            }
        },
        new Course()
        {
            Name = "BSc in (Hons) Finance",
            Uid = Guid.NewGuid(),
            Modules = new List<Module>()
            {
                new Module() { Name = "Financial Accounting", Uid = Guid.NewGuid() },
                new Module() { Name = "Global Business Environment", Uid = Guid.NewGuid() },
                new Module() { Name = "Introduction to Banking and Financial Markets", Uid = Guid.NewGuid() },
                new Module() { Name = "Financial Mathematics and Principles", Uid = Guid.NewGuid() },
                new Module() { Name = "Fixed Income Securities", Uid = Guid.NewGuid() },
                new Module() { Name = "Investments and Risk Management", Uid = Guid.NewGuid() },
                new Module() { Name = "Applied Corporate Finance", Uid = Guid.NewGuid() },
                new Module() { Name = "Global Financial Markets and Institutions", Uid = Guid.NewGuid() }
            }
        },
        new Course()
        {
            Name = "BSc (Hons) in Economics With Finance",
            Uid = Guid.NewGuid(),
            Modules = new List<Module>()
            {
                new Module() { Name = "Exploring economics", Uid = Guid.NewGuid() },
                new Module() { Name = "Mathematics for Economists", Uid = Guid.NewGuid() },
                new Module() { Name = "Contemporary Issues in Global Economy", Uid = Guid.NewGuid() },
                new Module() { Name = "Corporate Finance", Uid = Guid.NewGuid() }
            }
        }
    };
}
