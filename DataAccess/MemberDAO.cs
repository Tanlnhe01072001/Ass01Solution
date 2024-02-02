using BusinessObject;
using System.Data;

namespace DataAccess
{
    public class MemberDAO
    {
        List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject
                {
                    MemberId = "M001",
                    MemberName = "Adam Doe",
                    City = "New York",
                    Country = "USA",
                    Email = "johndoe@email.com",
                    Password = "password123"
                },
                new MemberObject
                {
                    MemberId = "M002",
                    MemberName = "Jane Smith",
                    City = "London",
                    Country = "UK",
                    Email = "janesmith@email.com",
                    Password = "1234password"
                }
            };
        private static MemberDAO memberDAO = null;
        private static readonly object instanceLock = new object();

        private MemberDAO() { }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (memberDAO == null)
                    {
                        memberDAO = new MemberDAO();
                    }
                    return memberDAO;
                }
            }

        }

        public void insert(MemberObject member)
        {
            MemberObject existedMember = findById(member.MemberId);
            if (existedMember != null)
            {
                throw new Exception("Member already existed!");

            }
            else
            {
                members.Add(member);
            }
        }

        public List<MemberObject> findAll()
        {
            return new List<MemberObject>(members);
        }

        public List<MemberObject> SortByNameDescending()
        {
            return members.OrderByDescending(e => e.MemberName.ToLower()).ToList();
        }

        public MemberObject findById(string id)
        {
            return members.FirstOrDefault(m => m.MemberId.Equals(id));
        }

        public List<MemberObject> findByIdAndName(string memberId, string memberName)
        {
            return members.Where(m =>
                (string.IsNullOrEmpty(memberId) || m.MemberId.ToLower().Contains(memberId.ToLower())) &&
                (string.IsNullOrEmpty(memberName) || m.MemberName.ToLower().Contains(memberName.ToLower()))
            ).ToList();
        }

        public void update(MemberObject member)
        {
            MemberObject existingMember = findById(member.MemberId);
            if (existingMember != null)
            {
                existingMember.MemberName = member.MemberName;
                existingMember.Email = member.Email;
                existingMember.Password = member.Password;
                existingMember.City = member.City;
                existingMember.Country = member.Country;
            }
            else
            {
                throw new Exception("Member not found");
            }
        }
        public void remove(string id)
        {
            MemberObject existedMember = findById(id);
            if (existedMember == null) throw new Exception("Member not existed");
            members.Remove(existedMember);
        }

    }
}