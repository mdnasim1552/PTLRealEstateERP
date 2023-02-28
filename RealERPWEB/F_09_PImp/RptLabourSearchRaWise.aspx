<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLabourSearchRaWise.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptLabourSearchRaWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

            try {

                var gvspaysummary = $('#<%=this.gvsubbill.ClientID %>');
                gvspaysummary.Scrollable();

                var gvspaymentdetails = $('#<%=this.gvsubbill.ClientID %>');
              


                var gridViewScroll = new GridViewScroll({
                    

                });

              


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

            catch (e) {
                alert(e);
            }
        }
    </script>


    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lnkbtmfromtop {
            margin-top: 28px;
            margin-left: 5px;
        }

        .srchbtmfromtop {
            margin-top: 5px !important;
        }

        .grvContentarea {
        }

   

        .grvHeader {
            height: 38px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .table th, .table td {
            padding: 0px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    

 


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

                         
                        <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label" id="lblprjName" runat="server">Project Name</label>
                                <asp:DropDownList ID="ddlPrjlist" runat="server" CssClass="form-control form-control-sm  chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlPrjlist_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                         <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label" id="Label1" runat="server">Labour List</label>
                                <asp:DropDownList ID="ddlLabour" runat="server" CssClass="form-control form-control-sm  chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>



                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate"> Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                       

                        <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label" id="lblfloor" runat="server">Floor</label>
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" Width="200px"> </asp:DropDownList>
                            </div>
                        </div>
                        


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
   
              

                    <%--<div class="  table-responsive">--%>
                        <asp:GridView ID="gvsubbill" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" AllowPaging="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbillno" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno1")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                

                                <asp:TemplateField HeaderText="Bill Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbilldate" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "isudat")).ToString("dd-MMM-yyyy") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrp" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Labour">
                                                    <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Description of Labour" Width="180px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                        CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>
                                                    
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescription" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>

                                                 

                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>



                          

                               <%-- <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>--%>


                                
                                <asp:TemplateField HeaderText="R/A">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrp" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisurefno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblFgrp" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px">Total :</asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvissueqty" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFissueqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvfrate" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Amount">
                                    <ItemTemplate>

                                        <asp:Label ID="lblbillamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvafbillamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               



                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterStyle CssClass="grvHeader" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />


                        </asp:GridView>
                  <%--  </div>--%>
            

 


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>