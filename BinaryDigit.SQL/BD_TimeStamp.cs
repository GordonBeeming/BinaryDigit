using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;

using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedType(Format.UserDefined, IsByteOrdered = true, IsFixedLength = true, MaxByteSize = 51)]
public class BD_TimeStamp : IBinarySerialize, INullable, IComparable, IComparable<BD_TimeStamp> //, System.Xml.Serialization.IXmlSerializable 
{
    #region Constants

    private const int _dateByteLength = 17;

    private const string _dateFormat = "yyyyMMddHHmmssfff";

    #endregion

    #region Fields

    private DateTime _createDateTime;

    private DateTime? _deleteDateTime;

    private bool _isDeleted;

    private bool _isNull;

    private DateTime _updateDateTime;

    #endregion

    #region Constructors and Destructors

    public BD_TimeStamp()
    {
        this._createDateTime = DateTime.Now;
        this._updateDateTime = DateTime.Now;
        this._deleteDateTime = null;
        this._isDeleted = false;
        this._isNull = false;
    }

    #endregion

    #region Public Properties

    public static BD_TimeStamp Null
    {
        get
        {
            var h = new BD_TimeStamp();
            h._isNull = true;
            return h;
        }
    }

    public DateTime CreateDateTime
    {
        get
        {
            if (this._createDateTime == default(DateTime))
            {
                this._createDateTime = DateTime.Now;
            }
            return this._createDateTime;
        }
        private set
        {
            this._createDateTime = value;
        }
    }

    public DateTime? DeleteDateTime
    {
        get
        {
            return this._deleteDateTime;
        }
        private set
        {
            this._deleteDateTime = value;
        }
    }

    public bool IsDeleted
    {
        get
        {
            return this._isDeleted;
        }
        private set
        {
            this._isDeleted = value;
        }
    }

    public bool IsNull
    {
        get
        {
            return this._isNull;
        }
    }

    public DateTime UpdateDateTime
    {
        get
        {
            if (this._updateDateTime == default(DateTime))
            {
                this._updateDateTime = DateTime.Now;
            }
            return this._updateDateTime;
        }
        private set
        {
            this._updateDateTime = value;
        }
    }

    #endregion

    #region Public Methods and Operators

    public static BD_TimeStamp Parse(SqlString s)
    {
        if (s.IsNull)
        {
            return Null;
        }
        if (string.IsNullOrEmpty(s.Value))
        {
            return new BD_TimeStamp();
        }
        string[] parts = s.Value.Split(',');
        if (parts.Length != 3)
        {
            return Null;
        }

        var u = new BD_TimeStamp();

        u.CreateDateTime = DateTime.ParseExact(parts[0].Split(':')[1].Trim(), _dateFormat, CultureInfo.InvariantCulture);
        u.UpdateDateTime = DateTime.ParseExact(parts[1].Split(':')[1].Trim(), _dateFormat, CultureInfo.InvariantCulture);
        string delete = parts[2].Split(':')[1].Trim();
        if (delete == "N/A")
        {
            u.DeleteDateTime = null;
            u.IsDeleted = false;
        }
        else
        {
            u.DeleteDateTime = DateTime.ParseExact(delete, _dateFormat, CultureInfo.InvariantCulture);
            u.IsDeleted = true;
        }

        return u;
    }

    public int CompareTo(BD_TimeStamp other)
    {
        return string.Compare(this.ToString(), other.ToString());
    }

    public int CompareTo(object obj)
    {
        if (obj is BD_TimeStamp)
        {
            return this.CompareTo((BD_TimeStamp)obj);
        }
        return 0;
    }

    [SqlMethod(InvokeIfReceiverIsNull = false, IsMutator = true)]
    public BD_TimeStamp MarkAsDeleted()
    {
        this.DeleteDateTime = DateTime.Now;
        this.IsDeleted = true;
        return this;
    }

    public void Read(BinaryReader r)
    {
        if (r.BaseStream.Length > 0)
        {
            string temp = Encoding.ASCII.GetString(r.ReadBytes(_dateByteLength));
            this.CreateDateTime = new DateTime(Convert.ToInt32(CommonExtensions.ReadBetween(temp, 0, 3)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 4, 5)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 6, 7)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 8, 9)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 10, 11)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 12, 13)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 14, 16)));
            temp = Encoding.ASCII.GetString(r.ReadBytes(_dateByteLength));
            this.UpdateDateTime = new DateTime(Convert.ToInt32(CommonExtensions.ReadBetween(temp, 0, 3)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 4, 5)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 6, 7)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 8, 9)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 10, 11)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 12, 13)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 14, 16)));

            byte[] delete = r.ReadBytes(_dateByteLength);
            if (CommonExtensions.HasValueThatIsNot(delete, 0))
            {
                this.IsDeleted = false;
                this.DeleteDateTime = null;
            }
            else
            {
                temp = Encoding.ASCII.GetString(delete);
                this.IsDeleted = true;
                this.DeleteDateTime = new DateTime(Convert.ToInt32(CommonExtensions.ReadBetween(temp, 0, 3)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 4, 5)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 6, 7)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 8, 9)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 10, 11)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 12, 13)), Convert.ToInt32(CommonExtensions.ReadBetween(temp, 14, 16)));
            }
        }
    }

    public override string ToString()
    {
        return "Created : " + this.CreateDateTime.ToString(_dateFormat) +
               ", Updated : " + this.UpdateDateTime.ToString(_dateFormat) +
               ", Deleted : " + (this.IsDeleted ? this.DeleteDateTime.Value.ToString(_dateFormat) : "N/A");
    }

    public void Write(BinaryWriter w)
    {
        if (!this.IsNull)
        {
            //0-19
            w.Write(CommonExtensions.GetAsBytes(this.CreateDateTime.ToString(_dateFormat)));
            //20-39
            w.Write(CommonExtensions.GetAsBytes(DateTime.Now.ToString(_dateFormat)));
            //40-59
            if (this.IsDeleted)
            {
                w.Write(CommonExtensions.GetAsBytes(this.DeleteDateTime.Value.ToString(_dateFormat)));
            }
            else
            {
                w.Write(new byte[_dateByteLength]);
            }
        }
    }

    #endregion
}