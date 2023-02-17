using GWANet.Scanner.Extensions;
using Xunit;

namespace GWANet.Scanner.UnitTests.Extensions;

public sealed class DepthPointerTests
{
    [Fact]
    public unsafe void DepthPointer_Should_DereferenceFail()
    {
        var dummyNumber = 1;
        var numberPtr = &dummyNumber;
        var numberPtrPtr = &numberPtr;

        // Next level pointer invalidation (simulates a null/empty ptr)
        numberPtr = (int*) 0;

        var depthPtr = new DepthPointer<int>((int*)numberPtrPtr, 2);
        for (var x = 0; x < 50; x++)
        {
            dummyNumber = x;
            
            depthPtr.Dereference(out var isSuccess);
            
            // Let's check if it actually got there
            Assert.True(depthPtr.Depth == 2);
            Assert.False(isSuccess);
        }
    }
    
    [Fact]
    public unsafe void DepthPointer_Should_ReadDepth1Pointer()
    {
        const ushort pointerDepth = 1;
        var dummyNumber = 1;
        var numberPtr = &dummyNumber;

        var depthPtr = new DepthPointer<int>(numberPtr, pointerDepth);
        for (var i = 0; i < 50; i++)
        {
            dummyNumber = i;
            ref var newNumber = ref depthPtr.Dereference(out var isSuccess);
            
            Assert.True(isSuccess);
            Assert.Equal(dummyNumber, newNumber);
        }
    }

    [Fact]
    public unsafe void DepthPointer_Should_WriteDepth1Pointer()
    {
        const ushort pointerDepth = 1;
        var dummyNumber = 1;
        var numberPtr = &dummyNumber;

        var depthPtr = new DepthPointer<int>(numberPtr, pointerDepth);
        for (var i = 0; i < 100; i++)
        {
            ref var newNumber = ref depthPtr.Dereference(out var isSuccess);
            newNumber = i;
            
            Assert.True(isSuccess);
            Assert.Equal(dummyNumber, newNumber);
        }
    }
    
    [Fact]
    public unsafe void DepthPointer_Should_ReadDepth2Pointer()
    {
        const ushort pointerDepth = 2;
        var dummyNumber = 1;
        var numberPtr = &dummyNumber;
        var numberPtrPtr = &numberPtr;

        var depthPtr = new DepthPointer<int>((int*) numberPtrPtr, pointerDepth);
        for (var i = 0; i < 50; i++)
        {
            dummyNumber = i;
            ref var newNumber = ref depthPtr.Dereference(out var isSuccess);
            
            Assert.True(isSuccess);
            Assert.Equal(dummyNumber, newNumber);
        }
    }

    [Fact]
    public unsafe void DepthPointer_Should_WriteDepth2Pointer()
    {
        const ushort pointerDepth = 2;
        var dummyNumber = 1;
        var numberPtr = &dummyNumber;
        var numberPtrPtr = &numberPtr;

        var depthPointer = new DepthPointer<int>((int*)numberPtrPtr, pointerDepth);
        for (var i = 0; i < 50; i++)
        {
            ref var newNumber = ref depthPointer.Dereference(out var isSuccess);
            newNumber = i;
            
            Assert.True(isSuccess);
            Assert.Equal(dummyNumber, newNumber);
        }
    }
}