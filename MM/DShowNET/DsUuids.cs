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
/******************************************************
                  DirectShow .NET
		      netmaster@swissonline.ch
*******************************************************/
//				UUIDs from uuids.h

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{


	[ComVisible(false)]
public class FilterCategory		// uuids.h  :  CLSID_*
{
		/// <summary> CLSID_AudioInputDeviceCategory, audio capture category </summary>
	public static readonly Guid AudioInputDevice	= new Guid( 0x33d9a762,0x90c8,0x11d0,0xbd,0x43,0x00,0xa0,0xc9,0x11,0xce,0x86 );

		/// <summary> CLSID_VideoInputDeviceCategory, video capture category </summary>
	public static readonly Guid VideoInputDevice	= new Guid( 0x860BB310,0x5D01,0x11d0,0xBD,0x3B,0x00,0xA0,0xC9,0x11,0xCE,0x86 );
}



	[ComVisible(false)]
public class Clsid		// uuids.h  :  CLSID_*
{
		/// <summary> CLSID_SystemDeviceEnum for ICreateDevEnum </summary>
	public static readonly Guid SystemDeviceEnum			= new Guid( 0x62BE5D10,0x60EB,0x11d0,0xBD,0x3B,0x00,0xA0,0xC9,0x11,0xCE,0x86 );

		/// <summary> CLSID_FilterGraph, filter Graph </summary>
	public static readonly Guid FilterGraph		= new Guid( 0xe436ebb3, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> CLSID_CaptureGraphBuilder2, new Capture graph building </summary>
	public static readonly Guid CaptureGraphBuilder2	= new Guid( 0xBF87B6E1, 0x8C27, 0x11d0, 0xB3, 0xF0, 0x0, 0xAA, 0x00, 0x37, 0x61, 0xC5 );

		/// <summary> CLSID_SampleGrabber, Sample Grabber filter </summary>
	public static readonly Guid SampleGrabber	= new Guid( 0xC1F400A0, 0x3F08, 0x11D3, 0x9F, 0x0B, 0x00, 0x60, 0x08, 0x03, 0x9E, 0x37 );

		/// <summary> CLSID_DvdGraphBuilder,  DVD graph builder </summary>
	public static readonly Guid DvdGraphBuilder	= new Guid( 0xFCC152B7, 0xF372, 0x11d0, 0x8E, 0x00, 0x00, 0xC0, 0x4F, 0xD7, 0xC0, 0x8B );

}



	[ComVisible(false)]
public class MediaType		// MEDIATYPE_*
{
		/// <summary> MEDIATYPE_Video 'vids' </summary>
	public static readonly Guid Video		= new Guid( 0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIATYPE_Interleaved 'iavs' </summary>
	public static readonly Guid Interleaved	= new Guid( 0x73766169, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIATYPE_Audio 'auds' </summary>
	public static readonly Guid Audio		= new Guid( 0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIATYPE_Text 'txts' </summary>
	public static readonly Guid Text		= new Guid( 0x73747874, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIATYPE_Stream </summary>
	public static readonly Guid Stream		= new Guid( 0xe436eb83, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );
}

	[ComVisible(false)]
public class MediaSubType		// MEDIASUBTYPE_*
{
		/// <summary> MEDIASUBTYPE_YUYV 'YUYV' </summary>
	public static readonly Guid YUYV	= new Guid( 0x56595559, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIASUBTYPE_IYUV 'IYUV' </summary>
	public static readonly Guid IYUV	= new Guid( 0x56555949, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIASUBTYPE_DVSD 'DVSD' </summary>
	public static readonly Guid DVSD	= new Guid( 0x44535644, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71 );

		/// <summary> MEDIASUBTYPE_RGB1 'RGB1' </summary>
	public static readonly Guid RGB1	= new Guid( 0xe436eb78, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB4 'RGB4' </summary>
	public static readonly Guid RGB4	= new Guid( 0xe436eb79, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB8 'RGB8' </summary>
	public static readonly Guid RGB8	= new Guid( 0xe436eb7a, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB565 'RGB565' </summary>
	public static readonly Guid RGB565	= new Guid( 0xe436eb7b, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB555 'RGB555' </summary>
	public static readonly Guid RGB555	= new Guid( 0xe436eb7c, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB24 'RGB24' </summary>
	public static readonly Guid RGB24	= new Guid( 0xe436eb7d, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_RGB32 'RGB32' </summary>
	public static readonly Guid RGB32	= new Guid( 0xe436eb7e, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );


		/// <summary> MEDIASUBTYPE_Avi </summary>
	public static readonly Guid Avi	= new Guid( 0xe436eb88, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70 );

		/// <summary> MEDIASUBTYPE_Asf </summary>
	public static readonly Guid Asf	= new Guid( 0x3db80f90, 0x9412, 0x11d1, 0xad, 0xed, 0x0, 0x0, 0xf8, 0x75, 0x4b, 0x99 );
}


	[ComVisible(false)]
public class FormatType		// FORMAT_*
{
		/// <summary> FORMAT_None </summary>
	public static readonly Guid None		= new Guid( 0x0F6417D6, 0xc318, 0x11d0, 0xa4, 0x3f, 0x00, 0xa0, 0xc9, 0x22, 0x31, 0x96 );

		/// <summary> FORMAT_VideoInfo </summary>
	public static readonly Guid VideoInfo	= new Guid( 0x05589f80, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a );

		/// <summary> FORMAT_VideoInfo2 </summary>
	public static readonly Guid VideoInfo2	= new Guid( 0xf72a76A0, 0xeb0a, 0x11d0, 0xac, 0xe4, 0x00, 0x00, 0xc0, 0xcc, 0x16, 0xba );

		/// <summary> FORMAT_WaveFormatEx </summary>
	public static readonly Guid WaveEx		= new Guid( 0x05589f81, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a );

		/// <summary> FORMAT_MPEGVideo </summary>
	public static readonly Guid MpegVideo	= new Guid( 0x05589f82, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a );

		/// <summary> FORMAT_MPEGStreams </summary>
	public static readonly Guid MpegStreams	= new Guid( 0x05589f83, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a );

		/// <summary> FORMAT_DvInfo </summary>
	public static readonly Guid DvInfo		= new Guid( 0x05589f84, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a );
}




	[ComVisible(false)]
public class PinCategory		// PIN_CATEGORY_*
{
		/// <summary> PIN_CATEGORY_CAPTURE </summary>
	public static readonly Guid Capture		= new Guid( 0xfb6c4281, 0x0353, 0x11d1, 0x90, 0x5f, 0x00, 0x00, 0xc0, 0xcc, 0x16, 0xba );

		/// <summary> PIN_CATEGORY_PREVIEW </summary>
	public static readonly Guid Preview		= new Guid( 0xfb6c4282, 0x0353, 0x11d1, 0x90, 0x5f, 0x00, 0x00, 0xc0, 0xcc, 0x16, 0xba );
}



} // namespace DShowNET
