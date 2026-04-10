using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.subjects;

namespace POSProject.services
{
    public class SubjectService
    {
        private readonly ISubjectRepository _repo;
        public SubjectService(ISubjectRepository repo)
        {
            _repo = repo;
        }
        public List<string> GetSubjectNames() => _repo.GetAllNames();
        public SubjectModel GetByName(string pershkrimi) => _repo.GetByPershkrimi(pershkrimi);
    }
}
