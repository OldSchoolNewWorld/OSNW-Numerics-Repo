Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit
Imports OSNW.Numerics

Namespace Complex

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToString

    Public Class ToStringAIB

        <Theory>
        <InlineData(125.125, 50.5, "125.125+i50.5")>
        <InlineData(-125.125, 50.5, "-125.125+i50.5")>
        <InlineData(125.125, -50.5, "125.125-i50.5")>
        <InlineData(-125.125, -50.5, "-125.125-i50.5")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-i3.4028234663852886E+38")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToStringAIB
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringAIB

    Public Class ToStringABI

        <Theory>
        <InlineData(125.125, 50.5, "125.125+50.5i")>
        <InlineData(-125.125, 50.5, "-125.125+50.5i")>
        <InlineData(125.125, -50.5, "125.125-50.5i")>
        <InlineData(-125.125, -50.5, "-125.125-50.5i")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-3.4028234663852886E+38i")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToStringABI()
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringABI

End Namespace ' Complex
