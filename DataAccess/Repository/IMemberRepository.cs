using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> findAll();
        MemberObject findById(string id);

        List<MemberObject> findByIdAndName(string id, string name);
        void insert(MemberObject member);

        void remove(string id);
        void update(MemberObject member);

        List<MemberObject> sortByNameDescending();

    }
}
