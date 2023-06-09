﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PoMrrBillStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptOrderSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

        }
        function bill() {
            OpenPOamt();
            ClosePOamt();
        }

        function OpenPOamt() {

            $('#mbillstatus').modal('toggle');
        }
        function ClosePOamt() {
            $('#mbillstatus').modal('hide');
        }

    </script>
    <style type="text/css">
        .table th, .table td {
            padding: 2px;
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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row pb-4">
                        <div class="col-md-2">
                            <asp:Label ID="Label15" runat="server" CssClass="form-label" Text="From"></asp:Label>

                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control inputtextbox"
                                TabIndex="7"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="To"></asp:Label>

                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control inputtextbox"
                                TabIndex="7"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblsup" runat="server" CssClass="form-label" Text="Supplier Name"></asp:Label>
                            <asp:DropDownList ID="ddlSubName" runat="server"
                                AutoPostBack="True" CssClass="form-control chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3 ml-2">
                            <asp:Label ID="lblprj" runat="server" CssClass="form-label"
                                Text="Project Name"></asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server"
                                CssClass="form-control chzn-select"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>



                        <div class="col-md-1 ml-2" style="margin-top: 21px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_OnClick" CssClass="btn btn-primary primaryBtn"
                                TabIndex="6">Ok</asp:LinkButton>
                        </div>




                    </div>
                </div>
            </div>
            <div class="card mt-2" style="min-height: 480px;">
                <div class="card-body">
                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvBillStatus" runat="server" AutoGenerateColumns="False" 
                                CssClass=" table-striped table-hover table-bordered grvContentarea" 
                                ShowFooter="True" AllowPaging="True"  PageSize="30" 
                                OnPageIndexChanging="gvBillStatus_PageIndexChanging">
                                <PagerSettings Position="Bottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblprjcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblsupcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lgprjName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lgsupName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="fgvsup" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right">Total :</asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="PO Amount">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lgvpoamt" runat="server" Height="16px" Style="text-align: right" OnClick="lgvpoamt_Click"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px" />

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="fgvPOamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR Amount">
                                        <ItemTemplate>


                                            <asp:LinkButton ID="lgvmrramt" runat="server" Height="16px" Style="text-align: right" OnClick="lgvmrramt_Click"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px" />
                                        </ItemTemplate>


                                        <FooterTemplate>
                                            <asp:Label ID="fgvmrramt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Amount">
                                        <ItemTemplate>


                                            <asp:LinkButton ID="lgvbillamt" runat="server" Height="16px" Style="text-align: right" OnClick="lgvbillamt_Click"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px" />
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="fgvInvoice" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" HorizontalAlign="Right" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
            <div class="modal fade" id="mbillstatus" data-bs-backdrop="static"  data-bs-keyboard="false" tabindex="-1" aria-labelledby="mbillstatus" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            
                             <asp:Label ID="lblstatus" runat="server" Font-Bold="true" class="modal-title"
                                ></asp:Label>

                            <asp:LinkButton ID="Close" OnClick="Close_Click" ToolTip="Close the Window" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewPObill" runat="server">
                                    <asp:GridView ID="gvpobill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNopo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: center"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpono" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>

                                                 

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Order Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpodate" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpoqty" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Order rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvporate" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>
                                                  <FooterTemplate>
                                                   <asp:Label ID="fgvordtotal" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right">Total</asp:Label>
                                                  </FooterTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpoamt" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                   <asp:Label ID="fgvordamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Matrials Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvponame" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="160px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Brand">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpobrand" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" HorizontalAlign="Right" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="ViewReceive" runat="server">
                                    <asp:GridView ID="gvReceive" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNomrr" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: center"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRR No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrrno" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="MRR Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrrdate" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrrqty" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrrrate" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   <asp:Label ID="fgvmrrtotal" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right">Total</asp:Label>
                                                  </FooterTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrramt" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>
                                                  <FooterTemplate>
                                                   <asp:Label ID="fgvmrramt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Matrials Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrcvname" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'
                                                        Width="170px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Brand">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrcvbrand" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" HorizontalAlign="Right" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="ViewBill" runat="server">
                                    <asp:GridView ID="gvbill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNobill" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: center"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillno" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Bill Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbilldate" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Matrial qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillqty" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Matrial Rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillrate" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>
                                                    <FooterTemplate>
                                                   <asp:Label ID="fgvbilltotal" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right">Total</asp:Label>
                                                  </FooterTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillamt" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>

                                                </ItemTemplate>

                                                     <FooterTemplate>
                                                   <asp:Label ID="fgvbillamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Matrials Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillname" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'
                                                        Width="170px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillbrand" runat="server" Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" HorizontalAlign="Right" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </asp:View>
                            </asp:MultiView>

                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
