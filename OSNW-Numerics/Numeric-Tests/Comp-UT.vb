Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit
Imports OSNW.Numerics

Namespace Complex

    ' Use this to run ad hoc tests.
    Public Class AdHoc
        '
        '
        '
        '
        '
    End Class

    Public Class General

        <Fact>
        Sub Complex_Constructor_WorksAsExpected()
            Dim Z As New System.Numerics.Complex(125.125, -50.5)
            Assert.Equal(125.125, Z.Real)
            Assert.Equal(-50.5, Z.Imaginary)
        End Sub

    End Class ' General

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")> ' Encourage exponential notation
        Sub Complex_ToString_WorksAsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

        <Fact>
        Sub Standard_Works_AsExpected()

            Dim c1 As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Custom formatting with AIB0:        " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AIB0}", c1)}"
            Dim S2 As String = $"Custom formatting with AIB3:        " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AIB3}", c1)}"
            Dim S3 As String = $"Custom formatting with ABI0:        " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABI0}", c1)}"
            Dim S4 As String = $"Custom formatting with ABI3:        " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABI3}", c1)}"

            Dim Expect1 As String = "Custom formatting with AIB0:        12-i15"
            Dim Expect2 As String = "Custom formatting with AIB3:        12.100-i15.400"
            Dim Expect3 As String = "Custom formatting with ABI0:        12-15i"
            Dim Expect4 As String = "Custom formatting with ABI3:        12.100-15.400i"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)
            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub

    End Class ' ToString

End Namespace ' Complex
