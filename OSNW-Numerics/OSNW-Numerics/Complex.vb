Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Runtime.CompilerServices

' TODO:
' Look into these to implement standard forms A+Bi and A+iB for Complex, then R+Xj and R+jX for Impedance.
'   REF: Complex Struct
'   https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-8.0
'   REF: System.Numerics.Complex struct
'   https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex
'   REF: Format a complex number
'   https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex#format-a-complex-number

''' <summary>
''' This module contains extension methods for the
''' <see cref="System.Numerics.Complex"/> type.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </summary>
''' <remarks>
''' The extension methods are used to convert a <c>Complex</c> to its equivalent
''' string representation in the standard form of A + iB or A + Bi.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </remarks>
Public Module Complex

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c
    Private Const CHARUPPERE As System.Char = "E"c

#Region "ToString"

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+iB.
    ''' </summary>
    ''' <returns>The A+iB standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringAIB(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{CHARI}{Math.Abs(aComplex.Imaginary)}"
    End Function ' ToStringAIB

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+Bi.
    ''' </summary>
    ''' <returns>The A+Bi standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringABI(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{Math.Abs(aComplex.Imaginary)}{CHARI}"
    End Function ' ToStringABI

#End Region ' "ToString"

End Module ' Complex
