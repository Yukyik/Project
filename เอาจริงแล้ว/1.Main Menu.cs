using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace เอาจริงแล้ว
{
    public partial class Form1 : Form
    {
        int[] seatlist =
            {
                0, 0 , 0, 0 ,0, 0, 0,
                0, 0 , 0, 0 ,0, 0, 0,
                0, 0 , 0, 0 ,0, 0, 0,
                0, 0 , 0, 0 ,0, 0, 0,
                0, 0 , 0, 0 ,0, 0, 0,
                0, 0 , 0, 0 ,0, 0, 0,
            };
        string[] Seat_name =
        {
            "A1","A2","A3","A4","A5","A6","A7",
            "B1","B2","B3","B4","B5","B6","B7",
            "C1","C2","C3","C4","C5","C6","C7",
            "D1","D2","D3","D4","D5","D6","D7",
            "E1","E2","E3","E4","E5","E6","E7",
            "F1","F2","F3","F4","F5","F6","F7",
        };
        int Movie_Selected = 0;
        int[] Time_edit = { 0, 0, 0 };
        int Slot_edit = 0;
        int Timeselected = 1;

        int[] movie_selected_array = { 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
        string[] Seat_String = { "", "seat1", "seat2", "seat3", "seat4", "seat5", "seat6", "seat7", "seat8", "seat9" };

        PrintPreviewDialog PrintPreview = new PrintPreviewDialog();
        PrintDocument PrintDoc = new PrintDocument();
        Bitmap memory_img;

        public Form1()
        {
            InitializeComponent();
            hide_menu_on_start();
        }

        private void hide_menu_on_start() //ปิดทุกอย่าง
        {
            Receipt_Panel.Visible = false;
            Cart_Payment_Panel.Visible = false;
            Movie_List.Enabled = false;
            Cart_Payment.Enabled = false;
            Receipt.Enabled = false;

            highA.Enabled = false;
            highB.Enabled = false;
            highC.Enabled = false;
            highD.Enabled = false;
            highE.Enabled = false;
            highF.Enabled = false;

            T1.Visible = false;
            T2.Visible = false;
            T3.Visible = false;
            TT1.Visible = false;
            TT2.Visible = false;
            TT3.Visible = false;
            TTT1.Visible = false;
            TTT2.Visible = false;
            TTT3.Visible = false;

            register_form.Visible = false;
            MainPanel.Visible = false;
            movie_panel.Visible = false;
            Seat_panel.Visible = false;
            Confirm_panel.Visible = false;
            Print_Panel_main.Visible = false;
            Maintance_panel.Visible = false;
            Help_panel.Visible = false;

            Maintance_button.Enabled = false;

            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
        }
        private void Showmenu(Panel Submenu) //สลับเมนูเปิดปิด
        {
            if (Submenu.Visible == false)
            {
                Hidemenu();
                Submenu.Visible = true;
            }
            else
                Submenu.Visible = false;
        }
        private void Hidemenu() //ซ่อนเมนู
        {
            if (Receipt_Panel.Visible == true)
                Receipt_Panel.Visible = false;

            if (Cart_Payment_Panel.Visible == true)
                Cart_Payment_Panel.Visible = false;

            if (MainMenu_Panel.Visible == true)
                MainMenu_Panel.Visible = false;
        }
        private void Hide_panel() //ซ่อน sub-menu
        {
            if (register_form.Visible == true)
                register_form.Visible = false;

            if (MainPanel.Visible == true)
                MainPanel.Visible = false;

            if (movie_panel.Visible == true)
                movie_panel.Visible = false;

            if (Seat_panel.Visible == true)
                Seat_panel.Visible = false;

            if (Confirm_panel.Visible == true)
                Confirm_panel.Visible = false;

            if (Print_Panel_main.Visible == true)
                Print_Panel_main.Visible = false;

            if (Maintance_panel.Visible == true)
                Maintance_panel.Visible = false;

            if (Help_panel.Visible == true)
                Help_panel.Visible = false;
        }
        private void ShowPanel(Panel Subpanel) //เปิด sub-menu
        {
            if (Subpanel.Visible == false)
            {
                Hide_panel();
                Subpanel.Visible = true;
            }
            else
                Subpanel.Visible = false;
        }
        private void Movie_Time_Click(object sender, EventArgs e)
        {
            List<Button> Button_time_user_select = new List<Button> // list ปุ่มเวลา 11:00 14:30 17:00
            {
                T1, T2, T3, TT1, TT2, TT3, TTT1, TTT2, TTT3
            };

            int i = 1;
            foreach (Button a in Button_time_user_select) //วนตรวจปุ่ม
            {
                if (sender == a)
                {
                    Timeselected = i;
                    break;
                }
                i += 1;
            }
            //
            if ((sender == T1) | (sender == T2) | (sender == T3))
            {
                TheaterNo.Text = "1";
                Theater_show_inconfirm.Text = "Theater 1";
            }
            else if ((sender == TT1) | (sender == TT2) | (sender == TT3))
            {
                TheaterNo.Text = "2";
                Theater_show_inconfirm.Text = "Theater 2";
            }
            else if ((sender == TTT1) | (sender == TTT2) | (sender == TTT3))
            {
                TheaterNo.Text = "3";
                Theater_show_inconfirm.Text = "Theater 3";
            }
            //
            if ((sender == T1) | (sender == TT1) | (sender == TTT1))
            {
                Show_round.Text = "11:00";
                textBox1.Text = "11:00 O'clock";
            }
            else if ((sender == T2) | (sender == TT2) | (sender == TTT2))
            {
                Show_round.Text = "14:30";
                textBox1.Text = "14:30 O'clock";
            }
            else if ((sender == T3) | (sender == TT3) | (sender == TTT3))
            {
                Show_round.Text = "17:00";
                textBox1.Text = "17:00 O'clock";
            }

            Seat_Check();
            ShowPanel(Seat_panel);
            Cart_Payment.Enabled = true;
            Cart_Payment_Panel.Visible = true;
            Confirm_button.Enabled = false;
        }
        private void Movie_Time_Selected(object sender, EventArgs e)
        {
            ShowPanel(movie_panel);
            Cart_Payment.Enabled = false;
            Cart_Payment_Panel.Visible = false;
            DB db = new DB();
            int a = 1;

            while (a < 4)
            {
                byte[] getImg = new byte[0];
                DataSet da_img = new DataSet();

                MySqlCommand Search_command = new MySqlCommand("SELECT `Name`, `Movie_length`, `Language`, `Image` , `Time1` ,`Time2` ,`Time3` FROM `movie` WHERE id =" + a, db.GetConnection());

                db.OpenConnection();

                MySqlDataReader data_rec = Search_command.ExecuteReader();

                while (data_rec.Read())
                {
                    if (a == 1)
                    {
                        Movie_name1.Text = data_rec.GetString(0);
                        Movie_time1.Text = data_rec.GetString(1) + " Mins";
                        Lang_1.Text = data_rec.GetString(2);
                        T1.Visible = data_rec.GetBoolean(4);
                        T2.Visible = data_rec.GetBoolean(5);
                        T3.Visible = data_rec.GetBoolean(6);
                    }
                    if (a == 2)
                    {
                        Movie_name2.Text = data_rec.GetString(0);
                        Movie_time2.Text = data_rec.GetString(1) + " Mins";
                        Lang_2.Text = data_rec.GetString(2);
                        TT1.Visible = data_rec.GetBoolean(4);
                        TT2.Visible = data_rec.GetBoolean(5);
                        TT3.Visible = data_rec.GetBoolean(6);
                    }
                    if (a == 3)
                    {
                        Movie_name3.Text = data_rec.GetString(0);
                        Movie_time3.Text = data_rec.GetString(1) + " Mins";
                        Lang_3.Text = data_rec.GetString(2);
                        TTT1.Visible = data_rec.GetBoolean(4);
                        TTT2.Visible = data_rec.GetBoolean(5);
                        TTT3.Visible = data_rec.GetBoolean(6);
                    }
                }

                db.CloseConnection();
                db.OpenConnection();

                MySqlDataAdapter adapter_info = new MySqlDataAdapter(Search_command);
                adapter_info.Fill(da_img);

                foreach (DataRow data_row in da_img.Tables[0].Rows)
                {
                    getImg = (byte[])data_row["Image"];
                }

                MemoryStream stream = new MemoryStream(getImg);

                if (a == 1)
                    Movie_list_pic1.Image = Image.FromStream(stream);
                if (a == 2)
                    Movie_list_pic2.Image = Image.FromStream(stream);
                if (a == 3)
                    Movie_list_pic3.Image = Image.FromStream(stream);

                db.CloseConnection();
                a++;
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            ShowPanel(Seat_panel);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ShowPanel(Maintance_panel);
        }
        private void Cart_Payment_Click(object sender, EventArgs e)
        {
            Showmenu(Cart_Payment_Panel);
        }
        private void Receipt_Click(object sender, EventArgs e)
        {
            Showmenu(Receipt_Panel);
        }
        private void MainMenu_Click(object sender, EventArgs e)
        {
            ShowPanel(panel2);
            Showmenu(MainMenu_Panel);
        }
        private void Login_Click(object sender, EventArgs e)
        {
            ShowPanel(MainPanel);
        }
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            ShowPanel(register_form);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowPanel(Help_panel);
        }
        private void Login_do(object sender, EventArgs e)
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user_pass` WHERE `username` = @user and `password` = @pass", db.GetConnection());
            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = UserIn.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PassIn.Text;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Success");
                Login_go.Enabled = false;
                Movie_List.Enabled = true;
                LoginButton.Enabled = false;
                RegisterButton.Enabled = false;
            }
            else
            {
                if (UserIn.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Username To Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (PassIn.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Password To Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if ((UserIn.Text == "Admin") && (PassIn.Text == "123456"))
            {
                Maintance_button.Enabled = true;
            }
        }
        private void Register_go(object sender, EventArgs e)
        {
            if ((CheckUsername()) && (Pass_rs.Text == Pass_rs_rs.Text) && (Pass_rs.Text != ""))
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `user_pass`(`Firstname`, `Lastname`, `Phone`, `Username`, `Password`) VALUES (@fn, @ln, @phone, @usn, @pass)", db.GetConnection());

                command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = FN.Text;
                command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = LN.Text;
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = PH.Text;
                command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = User_rs.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Pass_rs.Text;

                db.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Error");
                }

                db.CloseConnection();
            }
            else if ((Pass_rs.Text != Pass_rs_rs.Text) && (Pass_rs.Text != ""))
            {
                MessageBox.Show("Password not match");
            }
            else if (!CheckUsername())
            {
                MessageBox.Show("Username already been used");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        private bool CheckUsername() //เช็ค username ซ้ำ
        {
            DB db = new DB(); 

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user_pass` WHERE `Username` = @usn", db.GetConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = User_rs.Text;

            MySqlDataAdapter adapter_CheckUsername = new MySqlDataAdapter(command);
            adapter_CheckUsername.Fill(table);

            if (table.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SeatClick(object sender, EventArgs e)
        {
            int i = 0;
            Button b = (Button)sender;
            List<Button> Seat_list_click = new List<Button>
            {
                A1 ,A2 ,A3 ,A4 ,A5 ,A6 ,A7 ,
                B1 ,B2 ,B3 ,B4 ,B5 ,B6 ,B7 ,
                C1 ,C2 ,C3 ,C4 ,C5 ,C6 ,C7 ,
                D1 ,D2 ,D3 ,D4 ,D5 ,D6 ,D7 ,
                E1 ,E2 ,E3 ,E4 ,E5 ,E6 ,E7 ,
                F1 ,F2 ,F3 ,F4 ,F5 ,F6 ,F7
            };

            if (b.Text != "X") //วนตรวจที่นั่งที่เลือก
            {
                foreach (Button a in Seat_list_click)
                {
                    if(a == b)
                        seatlist[i] = 1;
                    i += 1;
                }
            }
            else
            {
                foreach (Button a in Seat_list_click)
                {
                    if (a == b)
                        seatlist[i] = 0;
                    i += 1;
                }
            }

            if (b.Text != "X")
                b.Text = "X";

            else if (b.Text == "X")
            {
                if ((b == A1) | (b == B1) | (b == C1) | (b == D1) | (b == E1) | (b == F1))
                    b.Text = "1";
                if ((b == A2) | (b == B2) | (b == C2) | (b == D2) | (b == E2) | (b == F2))
                    b.Text = "2";
                if ((b == A3) | (b == B3) | (b == C3) | (b == D3) | (b == E3) | (b == F3))
                    b.Text = "3";
                if ((b == A4) | (b == B4) | (b == C4) | (b == D4) | (b == E4) | (b == F4))
                    b.Text = "4";
                if ((b == A5) | (b == B5) | (b == C5) | (b == D5) | (b == E5) | (b == F5))
                    b.Text = "5";
                if ((b == A6) | (b == B6) | (b == C6) | (b == D6) | (b == E6) | (b == F6))
                    b.Text = "6";
                if ((b == A7) | (b == B7) | (b == C7) | (b == D7) | (b == E7) | (b == F7))
                    b.Text = "7";
            }
        }
        private void Seat_Check()
        {
            int k = 1;
            DB db = new DB();
            List<Button> Buttonlist = new List<Button>
                {
                        A1, A2 , A3, A4 ,A5, A6, A7,
                        B1, B2 , B3, B4 ,B5, B6, B7,
                        C1, C2 , C3, C4 ,C5, C6, C7,
                        D1, D2 , D3, D4 ,D5, D6, D7,
                        E1, E2 , E3, E4 ,E5, E6, E7,
                        F1, F2 , F3, F4 ,F5, F6, F7
                };

            string Temp_Seat_String = "";
            while (k < 10) //วนหาค่า id เวลาที่เลือก
            {
                if (Timeselected == k)
                {
                    Temp_Seat_String = Seat_String[k];
                    Movie_Selected = movie_selected_array[k];
                }
                k++;
            }

            int id_loop = 1;

            foreach (Button resetbutton in Buttonlist) //คืนค่าปุ่มทุกปุ่ม
            {
                resetbutton.Enabled = true;
            }

            foreach (Button a in Buttonlist) //ล็อคปุ่มที่นั่งที่ไม่ว่าง
            {
                MySqlCommand Search_command = new MySqlCommand("SELECT `Status_Bool` FROM " + Temp_Seat_String + " WHERE id =" + id_loop, db.GetConnection());
                db.OpenConnection();
                MySqlDataReader data_receive = Search_command.ExecuteReader();
                while (data_receive.Read())
                {
                    a.Enabled = data_receive.GetBoolean(0);
                }
                db.CloseConnection();
                id_loop += 1;
            }

        }
        private void Confirm_Click(object sender, EventArgs e)
        {
            int a = Movie_Selected;
            byte[] getImg = new byte[0];

            DB db = new DB();

            DataSet data_img = new DataSet();

            MySqlCommand Search_command = new MySqlCommand("SELECT `Name`, `Movie_length`, `Language`, `Image` FROM `movie` WHERE id = @AA", db.GetConnection());
            Search_command.Parameters.Add("@AA", MySqlDbType.Int32).Value = a;

            db.OpenConnection();
            
            MySqlDataReader data_rec = Search_command.ExecuteReader();
            
            while (data_rec.Read()) //แสดงชื่อ เวลา ภาษา ของหนังที่เลือกในหน้า confirm + recipt
            {
                Confirm_Movie.Text = data_rec.GetString(0);
                Confirm_time.Text = " " + data_rec.GetValue(1).ToString();
                Confirm_Lang.Text = data_rec.GetString(2);

                Receipt_name.Text = data_rec.GetString(0);
                Receipt_time.Text = data_rec.GetValue(1).ToString() +" Mins";
                Receipt_Lang.Text = data_rec.GetString(2);
            }

            db.CloseConnection();
            db.OpenConnection();

            MySqlDataAdapter adapter_info = new MySqlDataAdapter(Search_command); //แสดง รูป ของหนังที่เลือกในหน้า confirm + recipt
            adapter_info.Fill(data_img);

            foreach (DataRow dr in data_img.Tables[0].Rows)
            {
                getImg = (byte[])dr["Image"];
            }

            byte[] imgData = getImg;
            MemoryStream stream = new MemoryStream(imgData);
            Confirm_pic.Image = Image.FromStream(stream);
            Receipt_pic.Image = Image.FromStream(stream);
            
            db.CloseConnection();

            if (a == 1)
                Confirm_theater_no.Text = "Theater1";
            if (a == 2)
                Confirm_theater_no.Text = "Theater2";
            if (a == 3)
                Confirm_theater_no.Text = "Theater3";

            if (Seat_show_bigbox.Text == "")
            {
                int V = 0;
                int tempV = 0;
                int money;
                while (V < 42)
                {
                    if (seatlist[V] == 1)
                    {
                        tempV += 1;
                        Seat_show_bigbox.Text += Seat_name[V] + " "; //แสดงที่นั่งที่เลือกหน้า confirm
                        textBox3.Text += Seat_name[V] + " "; //แสดงที่นั่งที่เลือกหน้า print
                    }
                    V++;
                }
                Movie_List.Enabled = false; //lock movie list

                Seat_temp.Text = tempV.ToString(); //แสดงจำนวนที่นั่ง

                label25.Text = Movie_Selected.ToString(); //แสดงชื่อหนัง

                money = tempV * 120; //แสดงราคา
                Total.Text = money.ToString();
                textBox4.Text = money.ToString();

                DateTime localDate = DateTime.Now; //แสดงเวลาปัจจุบัน
                Datetime.Text = localDate.ToString();

                Confirm_button.Enabled = true;
                ShowPanel(Confirm_panel);
            }
        }
        private void Pay_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            int i = 1;
            int h;
            int loop42seat = 0;
            int loop7 = 1;
            string seat = "";
            List<Button> Seat_list_cancel = new List<Button>
            {
                A1 ,A2 ,A3 ,A4 ,A5 ,A6 ,A7 ,
                B1 ,B2 ,B3 ,B4 ,B5 ,B6 ,B7 ,
                C1 ,C2 ,C3 ,C4 ,C5 ,C6 ,C7 ,
                D1 ,D2 ,D3 ,D4 ,D5 ,D6 ,D7 ,
                E1 ,E2 ,E3 ,E4 ,E5 ,E6 ,E7 ,
                F1 ,F2 ,F3 ,F4 ,F5 ,F6 ,F7
            };

            if (sender == Cancel_confirm) //กดยกเลิกจอง ต้องคืนที่นั่งจาก X คือจองให้กลายเป็นเลขเพื่อกลับมาจองใหม่อีกรอบ
            {
                while (loop42seat < 42) //วนคืนต่า seatlist[] ให้เป็น 0 หมด
                {
                    seatlist[loop42seat] = 0;
                    loop42seat++;
                }
                foreach (Button a in Seat_list_cancel) 
                {
                    if (a.Text == "X") //ปุ่มไหนเป็น X ให้กลายเป็นเลขตาม loop7
                    {
                        a.Text = loop7.ToString();
                    }
                    loop7 += 1;
                    if (loop7 == 8) //ที่นั่งหมายเลข 1-7
                    {
                        loop7 = 1;
                    }
                }
                Movie_List.Enabled = true;
                Seat_show_bigbox.Text = "";
                textBox3.Text = "";
                ShowPanel(movie_panel);
                Cart_Payment.Enabled = false;
                Cart_Payment_Panel.Visible = false;
            }
            else
            {
                while (i < 42)
                {
                    if (seatlist[i - 1] == 1) //วนตรวจว่าเลือกที่นั่งที่ i-1 ไหม 
                    {
                        for(h=0;h<10;h++) //วนตรวจเวลาช่องที่เท่าไหร่ 0-9
                        {
                            if (Timeselected == h) 
                            {
                                seat = Seat_String[h];
                            }
                        }
                        MySqlCommand command = new MySqlCommand("UPDATE "+seat+" SET `Status_Bool`= 0 WHERE `Seat_no`="+i, db.GetConnection());
                        db.OpenConnection();
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    i++;
                }

                ShowPanel(Print_Panel_main); 
                Receipt_Panel.Visible = true;
                Receipt.Enabled = true;
                Cart_Payment.Enabled = false;
                Movie_List.Enabled = false;
                MainMenu.Enabled = false;
                Cart_Payment_Panel.Visible = false;
            }
        }
        private void button10_Click(object sender, EventArgs e) //เปิดหน้าต่างเลือกรูปหน้า Maintenance
        {
            OpenFileDialog openfile = new OpenFileDialog(); 
            openfile.Filter = "Choose Image (*.JPG;*.PNG;)|*.jpg;*.png"; //รับแค่ jbg png

            if (openfile.ShowDialog() == DialogResult.OK) //ถ้าผลเปิดได้เป็น true
            {
                Maintance_pic.Image = Image.FromFile(openfile.FileName);
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            int i = 0;
            string[] Temp_seat_string = { "", "", "" }; //รับ id โรงหนัง 3 ช่อง
            DB db = new DB();
             
            MemoryStream ms = new MemoryStream(); //รับรุป
            Maintance_pic.Image.Save(ms, Maintance_pic.Image.RawFormat);
            byte[] img = ms.ToArray();

            if (Slot_edit == 0) //กันเผื่อไม่ได้กดเลือก slot หนัง
            {
                MessageBox.Show("Error please select movie slot");
            }
            else
            {
                while (i < 42) //วน 42 ครั้งแต่ละโรงมี 42 ที่นั่งวนทีละ id
                {
                    if (Slot_edit == 1)
                    {
                        Temp_seat_string[0] = Seat_String[1];
                        Temp_seat_string[1] = Seat_String[2];
                        Temp_seat_string[2] = Seat_String[3];
                    }
                    if (Slot_edit == 2)
                    {
                        Temp_seat_string[0] = Seat_String[4];
                        Temp_seat_string[1] = Seat_String[5];
                        Temp_seat_string[2] = Seat_String[6];

                    }
                    if (Slot_edit == 3)
                    {
                        Temp_seat_string[0] = Seat_String[7];
                        Temp_seat_string[1] = Seat_String[8];
                        Temp_seat_string[2] = Seat_String[9];

                    }
                    int j = 0;
                    for (j=0;j<=2;j++) //วน 3 อัพเดตที่นั่งพร้อมกัน 3 table
                    {
                        MySqlCommand Update_Bool = new MySqlCommand("UPDATE " + Temp_seat_string[j] + " SET `Status_Bool`= 1 WHERE `Seat_no`= " + i, db.GetConnection());
                        db.OpenConnection();
                        Update_Bool.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    i++;
                }

                MySqlCommand max_picturesize_command = new MySqlCommand("SET GLOBAL max_allowed_packet=1024*1024*1024", db.GetConnection()); //ตั้งขนาดสูงสุดของการอัพรูป
                MySqlCommand update_movie_command = new MySqlCommand(" UPDATE `movie` SET `Name`= @NAME ,`Movie_length`= @TIME ,`Language`= @LANG ,`Time1`= @T1,`Time2`= @T2 ,`Time3`= @T3,`Image`= @IMG WHERE `id`=" + Slot_edit, db.GetConnection());
                update_movie_command.Parameters.Add("@NAME", MySqlDbType.VarChar).Value = Movie_to_edit.Text;
                update_movie_command.Parameters.Add("@TIME", MySqlDbType.Int32).Value = Duration_edit.Text;
                update_movie_command.Parameters.Add("@LANG", MySqlDbType.VarChar).Value = Lang_to_edit.Text;
                update_movie_command.Parameters.Add("@T1", MySqlDbType.Int32).Value = Time_edit[0];
                update_movie_command.Parameters.Add("@T2", MySqlDbType.Int32).Value = Time_edit[1];
                update_movie_command.Parameters.Add("@T3", MySqlDbType.Int32).Value = Time_edit[2];
                update_movie_command.Parameters.Add("@IMG", MySqlDbType.MediumBlob).Value = img;

                db.OpenConnection();
                max_picturesize_command.ExecuteNonQuery();
                update_movie_command.ExecuteNonQuery();
                db.CloseConnection();

                Duration_edit.Text = "";
                Lang_to_edit.Text = "";
                Movie_to_edit.Text = "";
                MessageBox.Show("Success!");
            }
        }
        private void SwapButton(object sender, EventArgs e) //สลับปุ่ม หน้า Maintenance
        {
            Button b = (Button)sender;
            if ((b.Text != "X") && (b.Text != "O"))
            {
                if (sender == Time1)
                {
                    Time1.Text = "O";
                    Time_edit[0] = 1;
                }
                if (sender == Time2)
                {
                    Time2.Text = "O";
                    Time_edit[1] = 1;
                }
                if (sender == Time3)
                {
                    Time3.Text = "O";
                    Time_edit[2] = 1;
                }
                if (sender == Slot_1)
                {
                    b.Text = "X";
                    Slot_2.Text = "2";
                    Slot_3.Text = "3";
                    Slot_edit = 1;
                }
                if (sender == Slot_2)
                {
                    b.Text = "X";
                    Slot_1.Text = "1";
                    Slot_3.Text = "3";
                    Slot_edit = 2;
                }
                if (sender == Slot_3)
                {
                    b.Text = "X";
                    Slot_1.Text = "1";
                    Slot_2.Text = "2";
                    Slot_edit = 3;
                }
            }
            else if (b.Text == "O")
            {
                if (sender == Time1)
                {
                    Time1.Text = "Select";
                    Time_edit[0] = 0;
                }
                if (sender == Time2)
                {
                    Time2.Text = "Select";
                    Time_edit[1] = 0;
                }
                if (sender == Time3)
                {
                    Time3.Text = "Select";
                    Time_edit[2] = 0;
                }
            }
        }
        // พิมพ์ตั๋ว
        private void Print_Click(object sender, EventArgs e) //ปุ่มปริ้น
        {
            Print_recipt(this.Print_Panel);
        }
        public void Print_recipt(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            PrintDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("custom", 730, 285); //ตั้งขนาดหน้ากระดาษ
            Print_Panel = pnl;
            GetPrintArea(pnl);
            PrintPreview.Document = PrintDoc;
            PrintDoc.PrintPage += new PrintPageEventHandler(PrintDoc_Printpage); //เรียกใช้ event จากฟังค์ชั่น PrintDoc_Printpage
            PrintPreview.ShowDialog(); //เรียกใช้หน้าปริ้น
        }
        public void PrintDoc_Printpage(object sender,PrintPageEventArgs e) //สร้างรุป
        {
            Rectangle PageArea = e.PageBounds;
            e.Graphics.DrawImage(memory_img, 0, 0);
        }
        public void GetPrintArea(Panel pnl) //รับขนาด panel
        {
            memory_img = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memory_img, new Rectangle(0,0,pnl.Width,pnl.Height));
        }
        //

        //ตรวจตัวหนังสือ
        private void NO_Alphabet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void Alphabet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        //
    }
}