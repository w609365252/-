using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace PhoneSearchClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            InitializeComponent();
            //利用反射设置DataGridView的双缓冲
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);
        }

        int totalCount = 0;
        int currCount = 0 ;
        int noSearchCount = 0;
        List<string> noSearchPhoneList=new List<string>();
        List<PhoneData.PhoneRecord> cacheLoadList = new List<PhoneData.PhoneRecord>();
        private void button1_Click(object sender, EventArgs e)
        {
            //string output;
            //output = pd.Lookup("14794299760").ToString();
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "excel(*.xls)|*.xls|excel(*.xlsx)|*.xlsx";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                Loading.Text = "数据加载中...";
                currCount = 0;
                noSearchCount = 0;
                noSearchPhoneList = new List<string>();
                ClearLoadList();
                lv_province.Clear();
                Task.Factory.StartNew((state) =>
                {
                    //excel文件路径
                    string filePath = openDlg.FileName;
                    //获取到工作簿
                    try
                    {
                        var workFile = NPOIExcelHelper.OpenWorkbook(filePath);

                        List<DataGridViewRow> dataGridViewRows = new List<DataGridViewRow>();
                        //获取行信息
                        List<string[]> phoneList = NPOIExcelHelper.ReadLines(workFile.GetSheetAt(0), 0, 0, 0);
                        NPOIExcelHelper.CloseWorkbook(workFile);
                        PhoneData pd = new PhoneData("phone.dat");
                        totalCount = phoneList.Count(m => !string.IsNullOrEmpty(m[0]) && m[0].StartsWith("1"));

                        phoneList.ForEach(m =>
                        {
                            if (m.Length > 0)
                            {
                                string mobile = m[0];
                                if (!string.IsNullOrEmpty(mobile) && mobile.StartsWith("1"))
                                {
                                    var model = pd.Lookup(mobile);
                                    string Province = model.Province;//省
                                    string City = model.City;//市
                                    string CardType = model.CardType;//运营商
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = mobile });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = Province });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = City });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = CardType });
                                    dataGridViewRows.Add(row);
                                    cacheLoadList.Add(model);
                                    if (string.IsNullOrEmpty(Province))
                                    {
                                        noSearchCount++;
                                        noSearchPhoneList.Add(mobile);
                                    }
                                    else currCount++;
                                }
                                else
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = mobile });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "错误格式" });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "未识别" });
                                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = "未识别" });
                                    dataGridViewRows.Add(row);
                                    cacheLoadList.Add(new PhoneData.PhoneRecord() {
                                        PhoneNum=mobile,
                                        Province= "错误格式",
                                        City = "未识别",
                                        CardType = "未识别",
                                    });
                                }
                            }
                        });
                        this.Invoke(new EventHandler(delegate
                        {
                            dataGridView1.Rows.AddRange(dataGridViewRows.ToArray());
                        }));
                        lv_province.Items.Add("全部");
                        lab_datalist.Text = currCount.ToString() + "条";
                        var groups=cacheLoadList.GroupBy(m => m.Province);
                        foreach (var group in groups)
                        {
                            lv_province.Items.Add(group.Key);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("请关闭打开的Excel再进行导入");
                    }
                }, dataGridView1);

                Task.Run(new Action(delegate ()
                {
                    RefreshTxt();
                }));
            }
        }

        void ReloadData(string Province="")
        {
            
            dataGridView1.Rows.Clear();
            List<DataGridViewRow> dataGridViewRows = new List<DataGridViewRow>();
            List<PhoneData.PhoneRecord> cacheList = cacheLoadList;
            if (!string.IsNullOrEmpty(Province))
            {
                cacheList = cacheList.FindAll(m => m.Province == Province);
            }
            lab_datalist.Text = cacheList.Count.ToString()+"条";
            cacheList.ForEach(m =>
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = m.PhoneNum });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = m.Province });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = m.City });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = m.CardType });
                dataGridViewRows.Add(row);
            });
            this.Invoke(new EventHandler(delegate
            {
                dataGridView1.Rows.AddRange(dataGridViewRows.ToArray());
            }));
        }

        public void RefreshTxt() {
            while (true) {
                if (totalCount > 0) {
                    Loading.BeginInvoke(new Action(delegate ()
                    {
                        Loading.Text = $"已加载{currCount}条/共{totalCount}条";
                    }));
                    ErrorPhoneTxt.BeginInvoke(new Action(delegate ()
                    {
                        ErrorPhoneTxt.Text = $"共{noSearchCount}未识别";
                    }));
                }
                Thread.Sleep(500);
            }
        }

        void ClearLoadList()
        {
            cacheLoadList.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
                //1创建工作簿 2创建工作表  3创建行  4创建单元格 5单元格赋值  
                //6合并单元格  7设置字体颜色  8设置单元格底色 9输出到文件
                //声明工作簿
                var wk = new XSSFWorkbook();
                //声明工作表
                var st = wk.CreateSheet();
                st.SetColumnWidth(0, 20 * 256);
                //创建行(默认从0行开始)
                var r = st.CreateRow(0);
                List<string> titleList = new List<string>()
                { 
                "手机号","省","市","运营商"
                };
                int n = 0;
                titleList.ForEach(m =>
                {
                //创建单元格(默认从0行开始)
                var c = r.CreateCell(n);
                //赋值
                c.SetCellValue(m);
                    n++;
                });

                int rowNum = 1;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var r1 = st.CreateRow(rowNum);
                    for (int i = 0; i < item.Cells.Count; i++)
                    {
                        var c = r1.CreateCell(i);
                        //赋值
                        c.SetCellValue(item.Cells[i].Value?.ToString() ?? "");
                    }
                    rowNum++;
                }
                
                string path = dialog.SelectedPath + "/" + DateTime.Now.Ticks + ".xlsx";

                //写入文件流              地址(完整路径)          创建          写
                var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                //写入
                wk.Write(fs);
                //关闭文件流
                fs.Close();

                MessageBox.Show("导出成功", "提示信息");
            }
        }

        

        private void ExportNoSearchPhone_Click(object sender, EventArgs e)
        {
            if (noSearchCount == 0)
            {
                MessageBox.Show("没有未识别的手机号");
                return;
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                //1创建工作簿 2创建工作表  3创建行  4创建单元格 5单元格赋值  
                //6合并单元格  7设置字体颜色  8设置单元格底色 9输出到文件
                //声明工作簿
                var wk = new XSSFWorkbook();
                //声明工作表
                var st = wk.CreateSheet();
                st.SetColumnWidth(0, 20 * 256);
                //创建行(默认从0行开始)
                var r = st.CreateRow(0);
                List<string> titleList = new List<string>()
                {
                "手机号"
                };
                int n = 0;
                titleList.ForEach(m =>
                {
                    //创建单元格(默认从0行开始)
                    var c = r.CreateCell(n);
                    //赋值
                    c.SetCellValue(m);
                    n++;
                });

                int rowNum = 1;

                foreach (string item in noSearchPhoneList)
                {
                    var r1 = st.CreateRow(rowNum);
                    var c = r1.CreateCell(0);
                    c.SetCellValue(item);
                    rowNum++;
                }

                string path = dialog.SelectedPath + "/未识别的手机号码" + DateTime.Now.Ticks + ".xlsx";

                //写入文件流              地址(完整路径)          创建          写
                var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                //写入
                wk.Write(fs);
                //关闭文件流
                fs.Close();

                MessageBox.Show("导出成功", "提示信息");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lv_province_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }

        private void lv_province_Click(object sender, EventArgs e)
        {
            try
            {
                string provine = lv_province.SelectedItems[0].Text;//第1列：微信号
                ReloadData(provine == "全部" ? "" : provine);
            }
            catch (Exception)
            {

            }
        }
    }
}
