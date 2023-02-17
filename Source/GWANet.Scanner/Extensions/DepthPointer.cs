using System.Runtime.CompilerServices;

namespace GWANet.Scanner.Extensions;

/// <summary>
/// Helper class for multi level pointers.
/// </summary>
/// <typeparam name="TStruct">Pointer type (int or long long usually)</typeparam>
public unsafe struct DepthPointer<TStruct> where TStruct : unmanaged
{
    /// <summary>
    /// First level depth memory address.
    /// </summary>
    public TStruct* Address { get; set; }
    /// <summary>
    /// Number of pointer dereferences.
    /// </summary>
    public ushort Depth { get; set; }

    /// <summary>
    /// Dereferences the pointer
    /// </summary>
    /// <param name="isSuccess">Indicates if at any level the address was null, if yes, then the method fails</param>
    /// <returns>TStruct type</returns>
    public ref TStruct Dereference(out bool isSuccess)
    {
        var address = Address;
        isSuccess = false;

        if (address == (TStruct*)0)
        {
            return ref Create(address);
        }

        for (var x = 0; x < Depth - 1; x++)
        {
            address = *(TStruct**)address;
            if (address == (TStruct*)0)
            {
                return ref Create(address);
            }
        }

        isSuccess = true;
        return ref Create(address);
    }

    public static ref TStruct Create(TStruct* ptr)
        => ref Unsafe.AsRef<TStruct>(ptr);
    // Temporarily public
    public DepthPointer(TStruct* address, ushort depth)
    {
        Address = address;
        Depth = depth;
    }
}