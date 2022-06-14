<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SubContracBillTracking.aspx.cs" Inherits="RealERPWEB.F_09_PImp.SubContracBillTracking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>
    

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
          <%--  $('#<%=this.gvsupstatus.ClientID%>').tblScrollable();--%>
            $(function () {
                $('[id*=chkSupCategory').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });

        }



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        
                        
                        <div class="col-md-3">
                            <div class="from-group">
                                <label class="control-label">Bill No</label>
                                <asp:DropDownList ID="ddlbillno" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>



                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
            
                 

                            <asp:GridView ID="gvconbilltrack" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="false"  CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvconbilltrack_RowDataBound"  >
                                <RowStyle />
                                <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                 <HeaderStyle HorizontalAlign="center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisueno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                 <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                 <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                    
                                          <asp:TemplateField HeaderText="R/A No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrano" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "radesc")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Floor Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgfloor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="350px"></asp:Label>
                                                </ItemTemplate>
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                  <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                 <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                  <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-Contractor Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvContractor01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="170px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            



                                        </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


           

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
