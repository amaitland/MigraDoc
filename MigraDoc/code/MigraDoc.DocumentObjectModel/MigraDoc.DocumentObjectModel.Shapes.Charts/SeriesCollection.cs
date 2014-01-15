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
using MigraDoc.DocumentObjectModel.Internals;

namespace MigraDoc.DocumentObjectModel.Shapes.Charts
{
  /// <summary>
  /// The collection of data series.
  /// </summary>
  public class SeriesCollection : DocumentObjectCollection
  {
    /// <summary>
    /// Initializes a new instance of the SeriesCollection class.
    /// </summary>
    internal SeriesCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SeriesCollection class with the specified parent.
    /// </summary>
    internal SeriesCollection(DocumentObject parent) : base(parent) { }

    /// <summary>
    /// Gets a series by it's index.
    /// </summary>
    public new Series this[int index]
    {
      get { return base[index] as Series; }
    }

    #region Methods
    /// <summary>
    /// Creates a deep copy of this object.
    /// </summary>
    public new SeriesCollection Clone()
    {
      return (SeriesCollection)DeepCopy();
    }

    /// <summary>
    /// Adds a new series to the collection.
    /// </summary>
    public Series AddSeries()
    {
      Series series = new Series();
      Add(series);
      return series;
    }
    #endregion

    #region Internal

	  
    #endregion
  }
}
