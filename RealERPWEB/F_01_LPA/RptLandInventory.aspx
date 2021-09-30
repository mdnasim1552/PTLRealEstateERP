<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLandInventory.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptLandInventory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">


        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });



        function pageLoaded() {


            try
            {
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {

                alert(e.message)
            }


        }

       
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">

                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Project</span>
                                        <%--<Label class=" lblmargin-top9px lblleftwidth80" >Zone</Label>--%>
                                        <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblzone" runat="server">Zone</label>--%>
                                    </div>

                                    <asp:DropDownList ID="ddlProject" runat="server" CssClass=" form-control chzn-select"  ClientIDMode="Static">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                          <div class="col-md-1">

                            <div class="form-group">
                                <asp:LinkButton id="lbtnShow" class="btn btn-primary"  OnClick="lbtnShow_Click" runat="server">Show</asp:LinkButton>
                                                         
                            </div>
                        </div>


                      
                    </div>
                    

                    
                    <div class="row">

                        <table id="gvlandinfo" class="table table-striped table-hover table-bordered grvContentarea">
                            <thead></thead>
                            <tbody></tbody>
                            <tfoot></tfoot>

                        </table>
                    </div>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
