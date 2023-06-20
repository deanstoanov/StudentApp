using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void RefreshTable()
        {
            var context = new StudentsDBEntities();
            dataGridView1.DataSource = context.Student.ToList();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        // Add student
        private void button1_Click(object sender, EventArgs e)
        {
            var context = new StudentsDBEntities();
            var student = new Student();
            student.Name = textBox1.Text;
            student.GPA = int.Parse(textBox2.Text);
            student.Id = context.Student.ToList().LastOrDefault().Id + 1;
            context.Student.Add(student);
            context.SaveChanges();
            RefreshTable();
        }

        // Update student
        private void button2_Click(object sender, EventArgs e)
        {
            var context = new StudentsDBEntities();

            DataGridViewRow row = dataGridView1.CurrentRow;
            int id = int.Parse(row.Cells[0].Value.ToString());

            var studentToUpdate = context.Student.Where(c => c.Id == id).FirstOrDefault();

            if (studentToUpdate != null)
            {
                studentToUpdate.Name = textBox1.Text;
                studentToUpdate.GPA = int.Parse(textBox2.Text);
                context.SaveChanges();
            }

            RefreshTable();
        }

        // Delete student
        private void button3_Click(object sender, EventArgs e)
        {
            var context = new StudentsDBEntities();
            DataGridViewRow row = dataGridView1.CurrentRow;
            int id = int.Parse(row.Cells[0].Value.ToString());
            var studentToDelete = context.Student.Where(c => c.Id == id).FirstOrDefault();

            if (studentToDelete != null)
            {
                context.Student.Remove(studentToDelete);
                context.SaveChanges();
            }

            RefreshTable();
        }

        public List<Student> ShowStudentsWithGPABiggerThan5()
        {
            using (StudentsDBEntities db = new StudentsDBEntities())
            {
                return db.Student.Where(student => student.GPA > 4).ToList();
            }
        }

        // Students with GPA > 5
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ShowStudentsWithGPABiggerThan5();
        }

        // All students
        private void button5_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        // TODO: delete this event
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
