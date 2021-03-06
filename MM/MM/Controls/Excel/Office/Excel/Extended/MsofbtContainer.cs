/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QiHe.Office.Excel
{
    public partial class MsofbtContainer
    {
        public List<EscherRecord> EscherRecords;

        public override void Decode()
        {
            MemoryStream stream = new MemoryStream(Data);
            EscherRecords = new List<EscherRecord>();
            while (stream.Position < Size)
            {
                EscherRecord record = EscherRecord.Read(stream);
                record.Decode();
                EscherRecords.Add(record);
            }
        }

        public TRecord FindChild<TRecord>() where TRecord : EscherRecord
        {
            foreach (EscherRecord record in EscherRecords)
            {
                if (record is TRecord)
                {
                    return record as TRecord;
                }
            }
            return null;
        }

        public List<TRecord> FindChildren<TRecord>() where TRecord : EscherRecord
        {
            List<TRecord> children = new List<TRecord>();
            foreach (EscherRecord record in EscherRecords)
            {
                if (record is TRecord)
                {
                    children.Add(record as TRecord);
                }
            }
            return children;
        }

    }
}
