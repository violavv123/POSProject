using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public interface ISubjectRepository
    {
        List<string> GetAllNames();
        SubjectModel GetByPershkrimi(string pershkrimi);
        DataTable GetAllSubjects();
        void Insert(SubjectModel subject);
        void Update(SubjectModel subject);
        void Delete(int id);
    }
}
