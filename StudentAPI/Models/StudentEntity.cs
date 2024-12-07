using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
    public class StudentEntity
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Class { get; set; }
    }
}
