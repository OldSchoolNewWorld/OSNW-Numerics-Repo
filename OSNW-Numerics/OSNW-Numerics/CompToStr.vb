Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Runtime.CompilerServices
Imports System.Numerics
Imports System.Text

Public Enum StandardFormOrder
    AiB ' A+iB order.
    ABi ' A+Bi order.
End Enum ' StandardFormOrder

Public Enum StandardFormSpacing
    Closed ' A+iB without spaces.
    Open ' A + iB with spaces.
End Enum ' StandardFormSpacing

Public Enum StandardForm
    AiBC ' A+iB Closed form.
    AiBO ' A + iB Open form.
    ABiC ' A+Bi Closed form.
    ABiO ' A + Bi Open form.
End Enum ' StandardForm

Public Class ComplexStandardFormatter
    Implements IFormatProvider, ICustomFormatter

    '   REF: Format a complex number
    ' https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex#format-a-complex-number

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c

    Public Function GetFormat(formatType As Type) As Object _
        Implements IFormatProvider.GetFormat

        If formatType Is GetType(System.ICustomFormatter) Then
            Return Me
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Implements the <see cref="System.ICustomFormatter.Format"/> method to
    ''' format a <see cref="System.Numerics.Complex"/> number in one of several
    ''' standard forms. The format string can be one of the following:
    ''' <list type="bullet">
    ''' <item><term>"AiBC"</term>"
    ''' <description>Closed form A+iB.</description></item>
    ''' <item><term>"AiBO"</term>"
    ''' <description>Open form A + iB.</description></item>
    ''' <item><term>"ABiC"</term>"
    ''' <description>Open form A + Bi.</description></item>
    ''' <item><term>"ABiO"</term>"
    ''' <description>Closed form A+Bi.</description></item>
    ''' </list>
    ''' The format string can also include a precision specifier, e.g. "AiBC3".
    ''' </summary>
    ''' <param name="standardFormat">Specifies the standard form ("AiBC",
    ''' "AiBO", "ABiC", "ABiO") to be applied.</param>
    ''' <param name="arg">An object to format.</param>
    ''' <param name="formatProvider">An object that supplies format information
    ''' about the current instance.</param>
    ''' <returns>The string representation of the value of arg, formatted as
    ''' specified by <paramref name="standardFormat"/> and
    ''' <paramref name="formatProvider"/>.</returns>
    Public Function Format(ByVal standardFormat As System.String,
                           ByVal arg As System.Object,
                           ByVal formatProvider As IFormatProvider) _
        As System.String _
        Implements ICustomFormatter.Format

        If TypeOf arg Is System.Numerics.Complex Then
            Dim Cplx As System.Numerics.Complex =
                DirectCast(arg, System.Numerics.Complex)

            ' Check if the format string has a precision specifier.
            Dim Precision As System.Int32
            Dim FmtString As System.String = System.String.Empty
            If standardFormat.Length > 4 Then
                Try
                    Precision = System.Int32.Parse(standardFormat.Substring(4))
                Catch e As System.FormatException
                    Precision = 0
                End Try
                FmtString = "N" + Precision.ToString()
            End If

            ' Do these in advance for readability and consistency below.
            Dim RStr As System.String = Cplx.Real.ToString(FmtString)
            Dim IStr As System.String =
                System.Math.Abs(Cplx.Imaginary).ToString(FmtString)
            Dim Sign As System.Char =
                If(Cplx.Imaginary < 0.0, CHARMINUS, CHARPLUS)

            ' Apply the selected format.
            If standardFormat.Substring(0, 4).Equals("AiBC",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return $"{RStr}{Sign}{CHARI}{IStr}"
            ElseIf standardFormat.Substring(0, 4).Equals("AiBO",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return $"{RStr} {Sign} {CHARI}{IStr}"
            ElseIf standardFormat.Substring(0, 4).Equals("ABiC",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return $"{RStr}{Sign}{IStr}{CHARI}"
            ElseIf standardFormat.Substring(0, 4).Equals("ABiO",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return $"{RStr} {Sign} {IStr}{CHARI}"
            Else
                Return Cplx.ToString(standardFormat, formatProvider)
            End If

        Else
            If TypeOf arg Is System.IFormattable Then
                Return DirectCast(
                    arg, System.IFormattable).ToString(standardFormat, formatProvider)
            ElseIf arg IsNot Nothing Then
                Return arg.ToString()
            Else
                Return String.Empty
            End If
        End If
    End Function

    ''' <summary>
    ''' Returns a standard form string for a
    ''' <see cref="System.Numerics.Complex"/> number in the specified
    ''' <paramref name="order"/>, with the specified <paramref name="spacing"/>.
    ''' The format string is constructed as follows:
    ''' <list type="bullet">
    ''' <item><term>Order</term>
    ''' <description>StandardFormOrder.AiB or StandardFormOrder.ABi.
    ''' </description></item>
    ''' <item><term>Spacing</term>
    ''' <description>StandardFormSpacing.Closed or StandardFormSpacing.Open.
    ''' </description></item>
    ''' </list>
    ''' </summary>
    ''' <param name="order">Specifies whether to export A+iB or A_Bi
    ''' order.</param>
    ''' <param name="spacing">Specifies whether to export closed A+iB or open
    ''' A + iB spacing.</param>
    ''' <returns>A standard format string for a <c>System.Numerics.Complex</c>,
    ''' using the specified <c>order</c> and <c>spacing</c>.</returns>
    Public Shared Function GetStandardFormat(ByVal order As StandardFormOrder,
        ByVal spacing As StandardFormSpacing) As System.String

        Dim Sb As New StringBuilder(4)
        Select Case order
            Case StandardFormOrder.AiB
                Sb.Append("AiB")
            Case StandardFormOrder.ABi
                Sb.Append("ABi")
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(order), order,
                    "Invalid StandardFormOrder specified.")
        End Select
        Select Case spacing
            Case StandardFormSpacing.Closed
                Sb.Append("C"c)
            Case StandardFormSpacing.Open
                Sb.Append("O"c)
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(spacing),
                    spacing, "Invalid StandardFormOrder specified.")
        End Select
        Return Sb.ToString()
    End Function ' GetStandardFormat

    ''' <summary>
    ''' Returns a standard form string for a
    ''' <see cref="System.Numerics.Complex"/> number in the specified
    ''' <paramref name="order"/>, with the specified <paramref name="spacing"/>
    ''' and <paramref name="precision"/>. The format string is constructed as
    ''' follows:
    ''' <list type="bullet">
    ''' <item><term>Order</term>
    ''' <description>StandardFormOrder.AiB or StandardFormOrder.ABi.
    ''' </description></item>
    ''' <item><term>Spacing</term>
    ''' <description>StandardFormSpacing.Closed or StandardFormSpacing.Open.
    ''' </description></item>
    ''' <item><term>Precision</term>
    ''' <description>A System.UInt16 value representing the number of decimal
    ''' places.
    ''' </description></item>
    ''' </list>
    ''' </summary>
    ''' <param name="order">Specifies whether to export A+iB or A_Bi
    ''' order.</param>
    ''' <param name="spacing">Specifies whether to export closed A+iB or open
    ''' A + iB spacing.</param>
    ''' <param name="precision">Specifies the number of decimal places to
    ''' provide in the result.</param>
    ''' <returns>A standard form string for a <c>System.Numerics.Complex</c>
    ''' number in the specified <c>order</c>, with the specified <c>spacing</c>
    ''' and <c>precision</c>.</returns>
    Public Shared Function GetStandardFormat(ByVal order As StandardFormOrder,
                                      ByVal spacing As StandardFormSpacing,
                                      ByVal precision As System.UInt16) _
                                      As System.String

        Dim sb As New StringBuilder(GetStandardFormat(order, spacing))
        sb.Append(precision)
        Return sb.ToString()
    End Function ' GetStandardFormat

End Class ' ComplexStandardFormatter

''' <summary>
''' This module contains extension methods for the
''' <see cref="System.Numerics.Complex"/> structure.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </summary>
''' <remarks>
''' The extension methods are used to convert a <c>Complex</c> to its equivalent
''' string representation in several standard forms.
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
    ''' equivalent string representation in the closed standardFormat of A+iB.
    ''' </summary>
    ''' <returns>The closed A+iB standardFormat form representation of the
    ''' current <c>Complex</c>.</returns>
    ''' <remarks>
    ''' The "Closed" versus "Open" nomenclature refers to the use of spaces
    ''' around the sign for the imaginary component.
    ''' </remarks>
    <Extension()>
    Public Function ToStringAiBC(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{CHARI}{Math.Abs(aComplex.Imaginary)}"
    End Function ' ToStringAiBC

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the open standardFormat of A + iB.
    ''' </summary>
    ''' <returns>The open A + iB standardFormat form representation of the
    ''' current <c>Complex</c>.</returns>
    ''' <remarks>
    ''' The "Closed" versus "Open" nomenclature refers to the use of spaces
    ''' around the sign for the imaginary component.
    ''' </remarks>
    <Extension()>
    Public Function ToStringAiBO(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{CHARI}{Math.Abs(aComplex.Imaginary)}"
    End Function ' ToStringAiBO

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the closed standardFormat of A+Bi.
    ''' </summary>
    ''' <returns>The closed A+Bi standardFormat form representation of the
    ''' current <c>Complex</c>.</returns>
    ''' <remarks>
    ''' The "Closed" versus "Open" nomenclature refers to the use of spaces
    ''' around the sign for the imaginary component.
    ''' </remarks>
    <Extension()>
    Public Function ToStringABiC(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{Math.Abs(aComplex.Imaginary)}{CHARI}"
    End Function ' ToStringABiC

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the open standardFormat of A + Bi.
    ''' </summary>
    ''' <returns>The open A + Bi standardFormat form representation of the
    ''' current <c>Complex</c>.</returns>
    ''' <remarks>
    ''' The "Closed" versus "Open" nomenclature refers to the use of spaces
    ''' around the sign for the imaginary component.
    ''' </remarks>
    <Extension()>
    Public Function ToStringABiO(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{Math.Abs(aComplex.Imaginary)}{CHARI}"
    End Function ' ToStringABiO

#End Region ' "ToString"

End Module ' Complex
