#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//   Klaus Potzesny (mailto:Klaus.Potzesny@pdfsharp.com)
//   David Stephensen (mailto:David.Stephensen@pdfsharp.com)
//
// Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Reflection;

namespace MigraDoc.DocumentObjectModel.Internals
{
  /// <summary>
  /// Meta class for document objects.
  /// </summary>
  public class Meta
  {
    /// <summary>
    /// Initializes a new instance of the DomMeta class.
    /// </summary>
    public Meta(Type documentObjectType)
    {
      AddValueDescriptors(this, documentObjectType);
    }

    /// <summary>
    /// Gets the object specified by name from dom.
    /// </summary>
    public object GetValue(DocumentObject dom, string name, GV flags)
    {
      int dot = name.IndexOf('.');
      if (dot == 0)
        throw new ArgumentException(DomSR.InvalidValueName(name));
      string trail = null;
      if (dot > 0)
      {
        trail = name.Substring(dot + 1);
        name = name.Substring(0, dot);
      }
      ValueDescriptor vd = this.vds[name];
      if (vd == null)
        throw new ArgumentException(DomSR.InvalidValueName(name));

      object value = vd.GetValue(dom, flags);
      if (value == null && flags == GV.GetNull)  //??? oder auch GV.ReadOnly?
        return null;

      //REVIEW DaSt: Sollte beim GV.ReadWrite das Objekt angelegt werden?
      if (trail != null)
      {
        if (value == null || trail == "")
          throw new ArgumentException(DomSR.InvalidValueName(name));
        DocumentObject doc = value as DocumentObject;
        if (doc == null)
          throw new ArgumentException(DomSR.InvalidValueName(name));
        value = doc.GetValue(trail, flags);
      }
      return value;
    }

    /// <summary>
    /// Determines whether the member of dom specified by name is null.
    /// If a member with the specified name does not exist an ArgumentException will be thrown.
    /// </summary>
    public virtual bool IsNull(DocumentObject dom, string name)
    {
      //bool isNull = false;
      int dot = name.IndexOf('.');
      if (dot == 0)
        throw new ArgumentException(DomSR.InvalidValueName(name));
      string trail = null;
      if (dot > 0)
      {
        trail = name.Substring(dot + 1);
        name = name.Substring(0, dot);
      }
      ValueDescriptor vd = this.vds[name];
      if (vd == null)
        throw new ArgumentException(DomSR.InvalidValueName(name));

      if (vd is NullableDescriptor || vd is ValueTypeDescriptor)
      {
        if (trail != null)
          throw new ArgumentException(DomSR.InvalidValueName(name));
        return vd.IsNull(dom);
      }
      DocumentObject docObj = (DocumentObject)vd.GetValue(dom, GV.ReadOnly);
      if (docObj == null)
        return true;
      if (trail != null)
        return docObj.IsNull(trail);
      else
        return docObj.IsNull();

      //      DomValueDescriptor vd = vds[name];
      //      if (vd == null)
      //        throw new ArgumentException(DomSR.InvalidValueName(name));
      //      
      //      return vd.IsNull(dom);
    }

    /// <summary>
    /// Determines whether all members of the specified dom are null. If dom contains no members IsNull
    /// returns true.
    /// </summary>
    public bool IsNull(DocumentObject dom)
    {
      int count = vds.Count;
      for (int index = 0; index < count; index++)
      {
        ValueDescriptor vd = vds[index];
        if (vd.IsRefOnly)
          continue;
        if (!vd.IsNull(dom))
          return false;
      }
      return true;
    }

    /// <summary>
    /// Gets the DomValueDescriptor of the member specified by name from the DocumentObject.
    /// </summary>
    public ValueDescriptor this[string name]
    {
      get { return this.vds[name]; }
    }

    /// <summary>
    /// Gets the DomValueDescriptorCollection of the DocumentObject.
    /// </summary>
    public ValueDescriptorCollection ValueDescriptors
    {
      get { return this.vds; }
    }
    ValueDescriptorCollection vds = new ValueDescriptorCollection();

    /// <summary>
    /// Adds a value descriptor for each field and property found in type to meta.
    /// </summary>
    static void AddValueDescriptors(Meta meta, Type type)
    {
      FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      foreach (FieldInfo fieldInfo in fieldInfos)
      {
#if DEBUG_
        string name = fieldInfo.Name;
        if (name == "parent")
          name.GetType();
#endif
        DVAttribute[] dvs = (DVAttribute[])fieldInfo.GetCustomAttributes(typeof(DVAttribute), false);
        if (dvs.Length == 1)
        {
          ValueDescriptor vd = ValueDescriptor.CreateValueDescriptor(fieldInfo, dvs[0]);
          meta.ValueDescriptors.Add(vd);
        }
      }

      PropertyInfo[] propInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      foreach (PropertyInfo propInfo in propInfos)
      {
#if DEBUG_
        string name = propInfo.Name;
        if (name == "Font")
          name.GetType();
#endif
        DVAttribute[] dvs = (DVAttribute[])propInfo.GetCustomAttributes(typeof(DVAttribute), false);
        if (dvs.Length == 1)
        {
          ValueDescriptor vd = ValueDescriptor.CreateValueDescriptor(propInfo, dvs[0]);
          meta.ValueDescriptors.Add(vd);
        }
      }
    }
  }
}