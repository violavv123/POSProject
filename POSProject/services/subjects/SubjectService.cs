using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.subjects;

namespace POSProject.services.subjects
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

        public DataTable GetAllSubjects()
        {
            return _repo.GetAllSubjects();
        }

        public void CreateSubject(SubjectModel subject)
        {
            _repo.Insert(subject);
        }

        public void UpdateSubject(SubjectModel subject)
        {
            _repo.Update(subject);
        }

        public void DeleteSubject(int id)
        {
            _repo.Delete(id);
        }
    }
}
