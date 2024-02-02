using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        public frmMemberManagement()
        {
            InitializeComponent();
        }
        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            loadMembers();
        }

        bool isValidEmail(string email)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, emailRegexPattern);
        }

        bool isValidDataInput()
        {
            return !String.IsNullOrEmpty(txtMemberId.Text) &&
                   !String.IsNullOrEmpty(txtMemberName.Text) &&
                   !String.IsNullOrEmpty(txtEmail.Text) &&
                   !String.IsNullOrEmpty(txtPassword.Text) &&
                   !String.IsNullOrEmpty(txtCountry.Text) &&
                   !String.IsNullOrEmpty(txtCity.Text);
        }

        private void cbCountry_Click(object sender, EventArgs e)
        {

        }

        private void cbCity_Click(object sender, EventArgs e)
        {

        }

        private void clearText()
        {
            txtMemberName.Text = string.Empty;
            txtMemberId.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
        }



        public void loadMembers()
        {
            if (checkSortByMemberName.Checked)//ktra xem co dc chọn hay k
            {
                var sortedMemberList = memberRepository.sortByNameDescending();
                dgvMemberList.DataSource = sortedMemberList;
            }
            else
            {
                var memberListAll = memberRepository.findAll();//truy xuất tất cả thành viên từ kho lưu trữ
                dgvMemberList.DataSource = memberListAll;
            }
        }

        private void checkSortByMemberName_CheckedChanged(object sender, EventArgs e)
        {
            loadMembers();
        }

        private void dgvMemberList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)// Kiểm tra xem ô được nhấp có ở hàng hợp lệ không
            {
                DataGridViewRow row = dgvMemberList.Rows[e.RowIndex];// Lấy DataGridViewRow tương ứng với hàng được click
                // Lấy giá trị từ các ô của hàng được nhấp chuột
                string memberId = row.Cells["MemberId"].Value.ToString();
                string memberName = row.Cells["MemberName"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string password = row.Cells["Password"].Value.ToString();
                string city = row.Cells["City"].Value.ToString();
                string country = row.Cells["Country"].Value.ToString();

                txtMemberId.Text = memberId;
                txtMemberName.Text = memberName;
                txtEmail.Text = email;
                txtPassword.Text = password;
                txtCity.Text = city;
                txtCountry.Text = country;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidDataInput())
                {
                    MessageBox.Show("Must input all fields");
                }
                else if (!isValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Invalid Email");
                }
                else
                {
                    string memberId = txtMemberId.Text;

                    MemberObject existingMember = memberRepository.findById(memberId);// Kiểm tra xem MemberId đã tồn tại chưa
                    if (existingMember != null)
                    {
                        MessageBox.Show("MemberId already exists!");
                        return;
                    }

                    MemberObject member = new MemberObject// tao memberobject ms
                    {
                        MemberId = memberId,
                        MemberName = txtMemberName.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                    };

                    memberRepository.insert(member);//chen tv vao kho luu tru
                    MessageBox.Show("Add member successfully!");
                    loadMembers();
                    clearText();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding member: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {   //Lấy thông tin thành viên từ các trường đầu vào
                string memberId = txtMemberId.Text;
                string memberName = txtMemberName.Text;
                string email = txtEmail.Text;
                string password = txtPassword.Text;
                string city = txtCity.Text;
                string country = txtCountry.Text;

                MemberObject existingMember = MemberDAO.Instance.findById(memberId);//ktra ton tai hay k
                if (existingMember == null)
                {
                    MessageBox.Show("Member does not exist!");
                    return;
                }
                MemberObject updatedMember = new MemberObject
                {
                    MemberId = memberId,
                    MemberName = memberName,
                    Email = email,
                    Password = password,
                    City = city,
                    Country = country
                };

                MemberDAO.Instance.update(updatedMember);
                MessageBox.Show("Member updated successfully!");
                loadMembers();
                clearText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating member: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string memberId = txtMemberId.Text;
                MemberObject existingMember = memberRepository.findById(memberId);
                if (existingMember == null)
                {
                    MessageBox.Show("Member does not exist!");
                    return;
                }

                memberRepository.remove(memberId);//Xóa thành viên có MemberId đã cho khỏi kho lưu trữ
                MessageBox.Show("Member deleted successfully!");
                loadMembers();
                clearText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting member: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string memberId = txtMemberId.Text;
                string memberName = txtMemberName.Text;
                List<MemberObject> member = memberRepository.findByIdAndName(memberId, memberName);//find tv dua tren id vs name dc cc

                if (member.Count == 0)
                {
                    MessageBox.Show("Member not found!");
                    clearText();
                    return;
                }
                dgvMemberList.DataSource = member;//Hiển thị kết quả tìm kiếm trong DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching member: " + ex.Message);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clearText();
        }
    }
}
