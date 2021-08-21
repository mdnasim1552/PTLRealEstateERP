<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AsitDevlop.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.AsitDevlop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script  type="text/javascript">
        $(document).ready(function () {
            try {
                $('#txtname').val("02");


                alert($('#txtname').val());
            }

            catch (e)
            {

                alert(e);
            }

           
        });

    </script>
  
   
    <div class="card card-fluid">
        <div class="row">
            <asp:TextBox ID="txtname" runat="server"  ClientIDMode="Static"  Style="display:none;"></asp:TextBox>
            <asp:LinkButton ID="lnkAdd" runat="server"  CssClass="btn btn-primary btn-xs" OnClick="lnkAdd_Click">Show</asp:LinkButton>

         


        </div>
        

    </div>
    


</asp:Content>

