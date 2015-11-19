using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FitnessClubMemberProject_Interface.DBLayer;
using System.Text.RegularExpressions;

namespace FitnessClubMemberProject_Interface
{
    public partial class Form1 : Form
    {
        private IMember memDb;

        public Form1()
        {
            memDb = new MemberDB();
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //add a new member
        private void btn_AddNew_Member_Click(object sender, EventArgs e)
        {
            if ((ValidateFields() && ValidateEmailAddress(txtMail.Text)))
            {
               if(memDb.AddNewMember(txtFN.Text, txtLN.Text, txtAddr.Text, txtCity.Text, txtPhone.Text, txtMail.Text) == 1)
               {
                   MessageBox.Show("A new Member has beeen successfully added to the database");
                   txtFN.Text = "";
                   txtLN.Text = "";
                   txtAddr.Text = "";
                   txtCity.Text = "";
                   txtPhone.Text = "";
                   txtMail.Text = "";
                   errorProviderAddMember.Clear();
                   this.Form1_Load(this, null);
               }
            }
            else
            {
                MessageBox.Show("Enter a valid email address");
            }


        }

        //Display Member list on page load
        private void Form1_Load(object sender, EventArgs e)
        {
            listViewAllMembers.Items.Clear();
            List<Member> listOfAllMembers;
            try
            {
                listOfAllMembers = memDb.GetAllMembers();
                if (listOfAllMembers.Count > 0)
                {
                    Member member;
                    for (int i = 0; i < listOfAllMembers.Count; i++)
                    {
                        member = listOfAllMembers[i];
                        listViewAllMembers.Items.Add(member.MemberID.ToString());
                        listViewAllMembers.Items[i].SubItems.Add(member.FirstName);
                        listViewAllMembers.Items[i].SubItems.Add(member.LastName);
                        listViewAllMembers.Items[i].SubItems.Add(member.Address);
                        listViewAllMembers.Items[i].SubItems.Add(member.City);
                        listViewAllMembers.Items[i].SubItems.Add(member.Phone);
                        listViewAllMembers.Items[i].SubItems.Add(member.Email);
                    }
                }
                else
                {
                    MessageBox.Show("There is currently no Member stored in the database");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, GetType().ToString());
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        //Find a member 
        private void Search_for_a_member_Click(object sender, EventArgs e)
        {
            int input = 0;
            bool result = int.TryParse(txtFindMember.Text, out input);
            if (result)
            {
                Member memObj = memDb.FindMemberByID(input);
                if (memObj != null)
                {
                    lblFnameLname.Visible = true;
                    lblAddrCity.Visible = true;
                    lblPhone.Visible = true;
                    lblEmail.Visible = true;

                    lblFnameLname.Text = memObj.FirstName + " " + memObj.LastName;
                    lblAddrCity.Text = memObj.Address + "," + " " + memObj.City;
                    lblPhone.Text = memObj.Phone;
                    lblEmail.Text = memObj.Email;

                    txtFindMember.Text = "";
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number");
            }
        }

        //Validate all textbox input
        public bool ValidateFields()
        {
            var controls = new[] { txtFN, txtLN, txtAddr, txtCity, txtPhone, txtMail };

            bool isValid = true;
            foreach (var control in controls.Where(e => String.IsNullOrEmpty(e.Text)))
            {
                errorProviderAddMember.SetError(control, "Please fill the required field");

                isValid = false;
            }


            return isValid;
        }
    
        //Validate if it is a valid email addre
        public bool ValidateEmailAddress(string emailAddr)
        {
            string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Match result = Regex.Match(emailAddr, regexPattern);
            return result.Success;
        }
        
    }
}
