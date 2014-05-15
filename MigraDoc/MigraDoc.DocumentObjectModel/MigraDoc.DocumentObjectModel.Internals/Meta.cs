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
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using MigraDoc.DocumentObjectModel.Internals;

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
	  Meta.AddValueDescriptors(this, documentObjectType);
	}

	/// <summary>
	/// Gets the meta object of the specified document object.
	/// </summary>
	/// <param name="documentObject">The document object the meta is returned for.</param>
	public static Meta GetMeta(DocumentObject documentObject)
	{
	  return documentObject.Meta;
	}

	/// <summary>
	/// Gets the object specified by name from dom.
	/// </summary>
	public object GetValue(DocumentObject dom, string name)
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
		return null;
	}

	/// <summary>
	/// Sets the member of dom specified by name to val.
	/// If a member with the specified name does not exist an ArgumentException will be thrown.
	/// </summary>
	public void SetValue(DocumentObject dom, string name, object val)
	{
	  
	}

	/// <summary>
	/// Determines whether this meta contains a value with the specified name.
	/// </summary>
	public bool HasValue(string name)
	{
		return false;
	}

	/// <summary>
	/// Sets the member of dom specified by name to null.
	/// If a member with the specified name does not exist an ArgumentException will be thrown.
	/// </summary>
	public void SetNull(DocumentObject dom, string name)
	{
	 
	}

	/// <summary>
	/// Determines whether the member of dom specified by name is null.
	/// If a member with the specified name does not exist an ArgumentException will be thrown.
	/// </summary>
	public virtual bool IsNull(DocumentObject dom, string name)
	{
		return false;
	}

	/// <summary>
	/// Sets all members of the specified dom to null.
	/// </summary>
	public virtual void SetNull(DocumentObject dom)
	{
	  
	}

	/// <summary>
	/// Determines whether all members of the specified dom are null. If dom contains no members IsNull
	/// returns true.
	/// </summary>
	public bool IsNull(DocumentObject dom)
	{
	  
	  return true;
	}

	

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
		
	  }

	  PropertyInfo[] propInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	  foreach (PropertyInfo propInfo in propInfos)
	  {
#if DEBUG_
		string name = propInfo.Name;
		if (name == "Font")
		  name.GetType();
#endif
		
	  }
	}
  }
}