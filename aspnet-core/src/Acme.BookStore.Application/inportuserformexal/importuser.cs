
using Acme.BookStore.Books;
using Acme.BookStore.ExtraProperty;
using Acme.BookStore.Identity;
using Acme.BookStore.Regrist;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Acme.BookStore.inportuserformexal
{

    public class Importuser : BookStoreAppService, ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly ICustomerIdentity _customerIdentity;

        string constr, Query, sqlconn;
        OleDbConnection Econ;

        public Importuser(ICustomerIdentity customerIdentity)
        {
            _customerIdentity = customerIdentity;
        }

        //protected new IdentityUserManager UserManager { get; }
        //protected new IIdentityUserRepository UserRepository { get; }
        //protected new IIdentityRoleRepository RoleRepository { get; }

        //protected new IOptions<IdentityOptions> IdentityOptions { get; }


        public async Task readexcelfileAsync()
        {
            try
            {
                Debug.WriteLine("Send to debug output.");
                int i = 0;
                ExcelConn(@"C:\Users\AbhijeetWarade\Desktop\Book1.xlsx");
                //var abc2 = File.ReadAllText(FilePath);

                Query = string.Format("Select * FROM [{0}]", "Sheet1$");
                OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                await Econ.OpenAsync();

                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                await Econ.CloseAsync();
                oda.Fill(ds);
                var abc = ds.Tables[0].AsEnumerable().Select(t => new value
                {
                    Name = t.Field<string>("Name"),
                    Email = t.Field<string>("Email"),
                    Gender = char.Parse(t.Field<string>("Gender")),
                    password = t.Field<string>("password"),
                    UserName = t.Field<string>("UserName"),
                    _Title = t.Field<double>("Title"),
                    IsActive = t.Field<Boolean>("IsActive"),
                    LockoutEnabled = t.Field<Boolean>("LockoutEnabled"),
                    Roles = t.Field<string>("Roles"),
                    Surname = t.Field<string>("Surname")


                }).ToList();


                foreach (var t in abc)
                {

                    var ssss = new IdentityUserCreateDto
                    {
                        Email = t.Email,
                        Name = t.Name,
                        Password = t.password,
                        IsActive = t.IsActive,
                        LockoutEnabled = t.LockoutEnabled,
                        RoleNames = t.Roles.Split(","),
                        Surname = t.Surname,
                        UserName = t.UserName,
                    };

                    ssss.SetProperty(UserConsts.GenderPropertyName, t.Gender.ToString());
                    var title = (Title)Enum.ToObject(typeof(Title), Convert.ToInt32(t._Title));
                    ssss.SetProperty(UserConsts.TitlePropertyName, title);

                    var abcddd = await _customerIdentity.CreateAsync(ssss);
                    i++;
                    Debug.WriteLine($"current record count - {i}");
                }
                //await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        private void ExcelConn(string FilePath)
        {

            constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
            Econ = new OleDbConnection(constr);

        }

        public async Task fakedataAsync()
        {
            var list  = new List<value>();
            for(int i = 0; i <= 20; i++)
            {

                var ssss = new value
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.First(),
                    password = "Abhij",
                    IsActive = true,
                    LockoutEnabled = true,
                    Roles = "user",
                    Surname = Faker.Name.Last(),
                    UserName = Faker.Internet.UserName(),
                    Gender = (char)Faker.Enum.Random<Gender>(),
                    _Title = 1
                    
                };

                list.Add(ssss);
                
                //ssss.SetProperty(UserConsts.GenderPropertyName, Faker.Enum.Random<Gender>().ToString());
                //var title = (Title)Enum.ToObject(typeof(Title), 1);
                //ssss.SetProperty(UserConsts.TitlePropertyName, title);

                //var abcddd = await _customerIdentity.CreateAsync(ssss);
            }
            var abcc =ConvertToDataTable<value>(list);
        }

        private DataTable ConvertToDataTable<T>(List<T> models)
        {
            // creating a data table instance and typed it as our incoming model   
            // as I make it generic, if you want, you can make it the model typed you want.  
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties of that model  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties              
            // Adding Column name to our datatable  
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names    
                dataTable.Columns.Add(prop.Name);
            }
            // Adding Row and its value to our dataTable  
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows    
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable    
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
    public class value
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double _Title { get; set; }
        public char Gender { get; set; }
        public bool IsActive { get; set; }
        public bool LockoutEnabled { get; set; }
        public string password { get; set; }
        public string Roles { get; set; }
        public string Surname { get; set; }

    }

    public enum Gender
    {

        Female = 'F',
        Male = 'M'
    }
}
