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

namespace QiHe.Office.CompoundDocumentFormat
{
    public class CompoundFileHeader : FileHeader
    {
        public new static readonly byte[] FileTypeIdentifier = new byte[8] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };
        /// <summary>
        /// Create a CompoundFileHeader with default values.
        /// </summary>
        public CompoundFileHeader()
        {
            base.FileTypeIdentifier = FileTypeIdentifier;
            FileIdentifier = Guid.NewGuid();
            RevisionNumber = 0x3E;
            VersionNumber = 0x03;
            ByteOrderMark = Endianness.LittleEndian;
            SectorSizeInPot = 9;
            ShortSectorSizeInPot = 6;
            UnUsed10 = new byte[10];
            UnUsed4 = new byte[4];
            MinimumStreamSize = 4096;
            FirstSectorIDofShortSectorAllocationTable = SID.EOC;
            FirstSectorIDofMasterSectorAllocationTable = SID.EOC;
            MasterSectorAllocationTable = new Int32[109];
        }
    }
}
