using System;
using System.Collections.Generic;
using System.Reflection;


namespace Project.Work
{

    interface ILog
    {
        int ID { get; set; }
    }
    class o_number : ILog
    {
        public int ID { get; set; }
        public string prop1 { get; set; }
    }
    public class LogOperations
    {
        //log operation
       Log logOperasyon = new Log();
        Shoe_DbEntities db = new Shoe_DbEntities();


        public void Add(object obj)
        {
            //add
            logOperasyon.islem = 1;
            //tbl id

            //tbl name
            logOperasyon.tbl_name = obj.GetType().Name;
            //tbl datetime
            logOperasyon.zaman = DateTime.Now;
            //tbl all prop
            List<PropertyInfo> props = new List<PropertyInfo>(obj.GetType().GetProperties());
            //tbl values 
            string ads = "";
            foreach (var item in props)
            {
                if (item.Name.Length < 25)
                {
                    ads += item.Name + ":";
                    ads += item.GetValue(obj, null) + ",";
                }
            }
            logOperasyon.yeni = ads;
            db.Log.Add(logOperasyon);
            db.SaveChanges();

        }
        public void Update(object old_o, object new_o)
        {
            //add
            logOperasyon.islem = 2;
            //tbl id
            logOperasyon.tbl_id = (int)GetValueProperty(old_o, "ID");
            //tbl name
            logOperasyon.tbl_name = old_o.GetType().Name;
            //tbl datetime
            logOperasyon.zaman = DateTime.Now;
            //tbl all prop

            //old all values 
            logOperasyon.eski = PropAndValue(old_o);
            //last all values 
            logOperasyon.yeni = PropAndValue(new_o);
            db.Log.Add(logOperasyon);
            db.SaveChanges();

        }
        public void Delete(object obj)
        {
            //add
            logOperasyon.islem = 3;
            //tbl id
            logOperasyon.tbl_id = (int)GetValueProperty(obj, "ID");
            //tbl name
            logOperasyon.tbl_name = obj.GetType().Name;
            //tbl datetime
            logOperasyon.zaman = DateTime.Now;
            //tbl all prop

            //old values
            logOperasyon.eski = PropAndValue(obj);
            //last all values 
            logOperasyon.yeni = "";
            db.Log.Add(logOperasyon);
            db.SaveChanges();

        }
        private void mainW()
        {
            o_number number = new o_number { ID = 5, prop1 = "Property" };
            Add(number);
            o_number up_num = new o_number();
            up_num.ID = number.ID;
            up_num.prop1 = number.prop1;
            Update(old_o: number, new_o: up_num);
            Delete(up_num);
        }
        private string PropAndValue(object obj)
        {
            string ads = "";
            List<PropertyInfo> props = new List<PropertyInfo>(obj.GetType().GetProperties());
            foreach (var item in props)
            {
                if (item.Name.Length < 25)
                {
                    ads += item.Name + ":";
                    ads += item.GetValue(obj, null) + ",";
                }
            }
            return ads;
        }


        private object GetValueProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
    }
}
