using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEReader
{
    using WORD = UInt16;
    using DWORD = UInt32;
    using LONG = UInt32;
    using BYTE = Byte;

    // 该结构体大小为 64byte。
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_DOS_HEADER
    {
        WORD e_magic;   // DOS 可执行文件标记。值为 0x5A4D,由于低位在前高位在后，所以存储为 0x4D5A。即 ASCII 字符为"MZ"。
        WORD e_cblp;
        WORD e_cp;
        WORD e_crlc;
        WORD e_cparhdr;
        WORD e_minalloc;
        WORD e_maxalloc;
        WORD e_ss;        // DOS 代码的初始化堆栈。
        WORD e_sp;        // DOS 代码的初始化堆栈指针SP。
        WORD e_csum;
        WORD e_ip;        // DOS 代码的初始化指令入口[指针IP]
        WORD e_cs;        // DOS 代码的初始化堆栈入口。
        WORD e_lfarlc;
        WORD e_ovno;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
        WORD[] e_res;
        WORD e_oemid;
        WORD e_oeminfo;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U2)]
        WORD[] e_res2;
        LONG e_lfanew;    // PE 文件头偏移位置。
    }

    struct IMAGE_NT_HEADERS32
    {
        DWORD Signature;  // 标识 PE 文件头部。值为 0x00004550,ASCII 码即 "PE00"。
        IMAGE_FILE_HEADER FileHeader;
        IMAGE_OPTIONAL_HEADER32 OptionalHeader;
    }

    struct IMAGE_FILE_HEADER
    {
        WORD Machine;   // 计算机的体系结构类型。
        WORD NumberOfSections; // 节数。这指示节表的大小, 它紧跟在标题后面。
        DWORD TimeDateStamp; // 图像的时间戳的低32位。这表示链接器创建映像的日期和时间。
        DWORD PointerToSymbolTable;  // 符号表的偏移量
        DWORD NumberOfSymbols;     // 符号表中的符号数。
        WORD SizeOfOptionalHeader; // 紧跟在该结构体后面的 IMAGE_OPTIONAL_HEADER32 结构体的大小。
        WORD Characteristics;   // 图像的特征。
    }

    struct IMAGE_OPTIONAL_HEADER32
    {
        WORD Magic;  // 图像文件的状态。
        BYTE MajorLinkerVersion; // 链接器的主要版本号。
        BYTE MinorLinkerVersion; // 链接器的次要版本号。
        DWORD SizeOfCode;  // 代码节的大小。
        DWORD SizeOfInitializedData; // 初始化数据节的大小。
        DWORD SizeOfUninitializedData; // 未初始化数据节的大小。
        DWORD AddressOfEntryPoint; // 指向入口点函数的指针。
        DWORD BaseOfCode; // 指向代码节的开头的指针
        DWORD BaseOfData; // 指向数据节开头的指针
        DWORD ImageBase;  // 在内存中加载图像的第一个字节的首选地址。
        DWORD SectionAlignment; // 在内存中加载的节的对齐方式。
        DWORD FileAlignment; // 图像文件中各节的原始数据的对齐方式
        WORD MajorOperatingSystemVersion; // 所需操作系统的主要版本号。
        WORD MinorOperatingSystemVersion; // 所需操作系统的次要版本号。
        WORD MajorImageVersion; // 图像的主要版本号。
        WORD MinorImageVersion; // 图像的次要版本号。
        WORD MajorSubsystemVersion; // 子系统的主要版本号。
        WORD MinorSubsystemVersion; // 子系统的次要要版本号。
        DWORD Win32VersionValue; // 必须为 0。
        DWORD SizeOfImage; // 图像的大小。
        DWORD SizeOfHeaders; // 头结构体的总大小，
        DWORD CheckSum;  // 图像文件校验和。
        WORD Subsystem; // 运行此映像所需的子系统。
        WORD DllCharacteristics;  // 图像的 DLL 特征。
        DWORD SizeOfStackReserve;  // 要为堆栈保留的字节数。
        DWORD SizeOfStackCommit;  // 要为堆栈提交的字节数。
        DWORD SizeOfHeapReserve;  // 要为本地堆保留的字节数。
        DWORD SizeOfHeapCommit;  // 要为本地堆提交的字节数。
        DWORD LoaderFlags;
        DWORD NumberOfRvaAndSizes;
        IMAGE_DATA_DIRECTORY[] DataDirectory; // IMAGE_NUMBEROF_DIRECTORY_ENTRIES
    }


    struct IMAGE_DATA_DIRECTORY
    {
        DWORD VirtualAddress;  // 表的相对虚拟地址。
        DWORD Size;  // 表的大小。
    }

    struct IMAGE_SECTION_HEADER
    {
        BYTE[] Name; // 用了定义区块名。 IMAGE_SIZEOF_SHORT_NAME
        DWORD PhysicalAddressOrVirtualSize; // 文件地址 或 加载到内存中的节的总大小
        DWORD VirtualAddress; // 加载到内存中的节的第一个字节的地址 (相对于映像基)。
        DWORD SizeOfRawData; // 磁盘上初始化数据的大小。
        DWORD PointerToRawData; // 指向 COFF 文件中第一页的文件指针。
        DWORD PointerToRelocations; // 指向该节的重新定位项开头的文件指针。如果没有迁移, 则此值为零。
        DWORD PointerToLinenumbers; // 指向该节的行号条目开头的文件指针。如果没有 COFF 行号, 则此值为零。
        WORD NumberOfRelocations; // 该节的搬迁条目数。此值为可执行图像的零。
        WORD NumberOfLinenumbers; // 节的行号条目数。
        DWORD Characteristics; // 图像的特征。
    }

}
