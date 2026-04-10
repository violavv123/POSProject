using System;
using System.Collections.Generic;
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
    }
}
