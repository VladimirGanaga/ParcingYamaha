using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ParcingYamaha.Dtos
{
    public class ChaptersDB
    {
        
        public int Id { get; set; }
        public int ModelsDBID { get; set; }
        public ModelsDB? modelsDB { get; set; }
        public string? partFile { get; set; }
        public string? chapter { get; set; }
        public string? chapterID { get; set; }

    }
}
