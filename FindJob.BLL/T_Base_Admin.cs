﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.BLL
{
    public class T_Base_Admin
    {
        public FindJob.DAL.T_Base_Admin dal = new DAL.T_Base_Admin();
        /// <summary>
        /// 企业模块
        /// </summary>
        public List<FindJob.Model.T_Base_Enterprise> GetList(int CurrentPage, int PageSize, string EPName)
        {
            return dal.GetList(CurrentPage, PageSize, EPName);
        }
        public int EPCount()
        {
            return dal.EPCount();
        }
        public int EPDelete(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.EPDelete(idstring);
        }
        /// <summary>
        /// 学生模块
        /// </summary>
        public List<FindJob.Model.T_Base_Student> GetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            return dal.GetList(CurrentPage, PageSize, StuName, SchoolName, MajorName, ClassName);
        }
        public int StuCount()
        {
            return dal.EPCount();
        }
        public int StuDelete(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.EPDelete(idstring);
        }
        /// <summary>
        /// 求职学生信息
        /// </summary>
        public List<FindJob.Model.T_Relation_ApplyJob> ApplyJobGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            return dal.ApplyJobGetList(CurrentPage, PageSize, StuName, SchoolName, MajorName, ClassName);
        }
        public int ApplyJobCount()
        {
            return dal.ApplyJobCount();
        }
        /// <summary>
        /// 企业信息审核
        /// </summary>
        public List<FindJob.Model.T_Base_Enterprise> EPCheckGetList(int CurrentPage, int PageSize, string EPName)
        {
            return dal.EPCheckGetList(CurrentPage, PageSize, EPName);
        }
        public int EPCheckCount()
        {
            return dal.EPCheckCount();
        }
        public int EPCheckPass(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.EPCheckPass(idstring);
        }
        /// <summary>
        /// 学生审核
        /// </summary>
        public List<FindJob.Model.T_Base_Student> StuCheckGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            return dal.StuCheckGetList(CurrentPage, PageSize, StuName, SchoolName, MajorName, ClassName);
        }
        public int StuCheckCount()
        {
            return dal.StuCheckCount();
        }
        public int StuCheckPass(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.StuCheckPass(idstring);
        }

        public List<FindJob.Model.T_Base_EI> EICheckGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            return dal.EICheckGetList(CurrentPage, PageSize, StuName, SchoolName, MajorName, ClassName);
        }
        public int EICheckCount()
        {
            return dal.EICheckCount();
        }
        public int EICheckPass(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.EICheckPass(idstring);
        }
        /// <summary>
        /// 学生就业信息
        /// </summary>
        public List<Model.T_Base_EI> EIGetList(int pageSize, int pageIndex, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            return dal.EIGetList(pageIndex, pageSize, StuName, SchoolName, MajorName, ClassName);
        }
        public int EICount()
        {
            return dal.EICount();
        }
        public int EIDelete(string[] ids)
        {
            //防止注入式漏洞
            string idstring = string.Join(", ", ids);
            return dal.EIDelete(idstring);
        }

        public void importStudent(string urlPath)
        {
            
            using (ExcelPackage package = new ExcelPackage(new FileStream(urlPath, FileMode.Open)))
            {

                List<Model.T_Base_Student> lst = new List<Model.T_Base_Student>();
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                for (int i = sheet.Dimension.Start.Row, RowEnd = sheet.Dimension.End.Row; i <= RowEnd; i++)
                {
                    Model.T_Base_Student stu = new Model.T_Base_Student();
                    stu.IdCard = sheet.Cells[i, 1].Value.ToString();//GetValue(sheet, m, j);
                    stu.Name = sheet.Cells[i, 2].Value.ToString();
                    stu.Gender = sheet.Cells[i, 3].Value.ToString();
                    stu.Phone = sheet.Cells[i, 4].Value.ToString();
                    stu.School = sheet.Cells[i, 5].Value.ToString();
                    stu.Major = sheet.Cells[i, 6].Value.ToString();
                    stu.Class = sheet.Cells[i, 7].Value.ToString();       
                    lst.Add(stu);
                }
                FindJob.DAL.T_Base_Admin dal = new DAL.T_Base_Admin();
                dal.ImportStudent(lst);
            }
            

        }
    }
}