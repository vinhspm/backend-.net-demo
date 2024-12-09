﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// đánh dấu property là primary key
    /// author: vinhkt (30/09/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey: Attribute
    {

    }

    /// <summary>
    /// đánh dấu property là không được để trống
    /// author: vinhkt (30/09/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {

    }

    /// <summary>
    /// đánh dấu property là không được trùng
    /// author: vinhkt (30/09/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicate : Attribute
    {

    }

    /// <summary>
    /// đánh dấu property là định dạng email
    /// author: vinhkt (30/09/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : Attribute
    {

    }


    /// <summary>
    /// đánh dấu property là xuất hiện trong sheet
    /// author: vinhkt (05/10/2022)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowInSheetAttribute: Attribute
    {

    }

    /// <summary>
    /// đánh dấu property là không phải cột
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotColumnAttribute : Attribute
    {

    }
}
