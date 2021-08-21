<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpStdKpi.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.EmpStdKpi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function loadModal() {
            $('#exampleModal3').modal('show');
        }
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
    

    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-5 pading5px ">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass="ddlPage">
                                </asp:DropDownList>

                                <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="ddlPage" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="70px">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem Selected="True">20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>

                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-5 pading5px ">
                                <asp:CheckBox ID="chkUpload" runat="server" TabIndex="10" Text="Upload From Marketing" CssClass="btn btn-primary checkBox" Visible="false" />
                                  <asp:CheckBox ID="chkcopy" runat="server" Visible="false" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" />
                                
                            </div>


                            <div class="col-md-2 pading5px asitCol3">
                            </div>
                        </div>
                        <asp:Panel ID="pnlCopy" runat="server" Visible="false" Style=" border:1px solid blue;">
                        
                         <div class="form-group">
                            <div class="col-md-5 pading5px ">
                                <asp:Label ID="lblPrevious" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlperyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass="ddlPage125">
                                </asp:DropDownList>

                                


                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnCopy" runat="server" Text="Copy From" OnClick="lbtnCopy_Click" CssClass="btn btn-primary checkBox" TabIndex="9"></asp:LinkButton>
                                </div>
                            </div>
                           
                        </div>
                            </asp:Panel>

                    </div>
                </fieldset>

               
                    <asp:GridView ID="gvStdKpi" runat="server" AllowPaging="True" Width="200px" OnRowDeleting="gvStdKpi_RowDeleting"
                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowCreated="gvStdKpi_RowCreated"
                        CssClass="table table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvStdKpi_PageIndexChanging">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="10px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterTemplate>


                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:CommandField ShowDeleteButton="True" />

                            <asp:TemplateField HeaderText="Empid" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpid" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empid")) %>'
                                        Width="100px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Employee Name">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpname" runat="server"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="190px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>

                                    
                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lnkTotal_Click">Total</asp:LinkButton>

                                    <asp:LinkButton ID="lnkappupdate" runat="server" CssClass="btn  btn-danger primarygrdBtn" OnClick="lnkappupdate_Click">Update</asp:LinkButton>


                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target<br/>Sales">
                                <ItemTemplate>
                                    <asp:label ID="txtgvsalTarget" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star1")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFsTarget" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target<br/>Collection">
                                <ItemTemplate>
                                    <asp:label ID="txtgvcollTarget" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star2")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFCollTarget" runat="server" Style="text-align: right; color: black;" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                         <%--   <asp:BoundField DataField="star3" HeaderText="Offer" ItemStyle-Width="150" />
                            <asp:BoundField DataField="star4" HeaderText="Visit" ItemStyle-Width="150" />
                            <asp:BoundField DataField="star5" HeaderText="Call" ItemStyle-Width="150" />
                            <asp:BoundField DataField="star6" HeaderText="Others" ItemStyle-Width="150" />
                          --%>


                             <asp:TemplateField HeaderText="Offer">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtaroffer" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star3")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtaroffer" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Visit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtarvisit" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star4")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtarvisit" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Call">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtarcall" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star5")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtarcall" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvtarothers" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star6")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFtarothers" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvstar7" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star7")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFstar7" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvstar8" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"star8")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFstar8" runat="server" Style="text-align: right; color: black;" Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Sales">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindsale" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval1")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Collection">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindColl" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval2")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Offer">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindoffer" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval3")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Visit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindVisit" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval4")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Call">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindCall" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval5")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvindothers" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval6")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsval7" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval7")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsval8" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"sval8")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Actual Others">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvacothers" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"acothers")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>









                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

               


                <div class="modal fade AsitModal" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title" id="myModalLabel">Employee KPI Group Setup</h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">

                                    <ContentTemplate>


                                        <fieldset class="scheduler-border fieldset_B">

                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:LinkButton ID="btnComp" runat="server" OnClick="btnComp_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Company Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtComp" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlCompName" runat="server" AutoPostBack="True" CssClass="ddlPage235 inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1 asitColPad">
                                                        <asp:LinkButton ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                                                    </div>
                                                    <div class="clearfix"></div>


                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">

                                                        <asp:LinkButton ID="btnDpt" runat="server" OnClick="btnDpt_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Dept Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtDpt" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlDptcode" runat="server" AutoPostBack="True" CssClass="ddlPage235 inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <div class="msgHandSt">
                                                            <asp:Label ID="lblmsg01" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblgrp" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="clearfix"></div>


                                                </div>
                                                <%--<div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol2">

                                                        <asp:LinkButton ID="btnSircode" runat="server" OnClick="btnSircode_Click" ToolTip="Click for Search" TabIndex="4" CssClass="lblTxt lblName">Activity Name:</asp:LinkButton>

                                                        <asp:TextBox ID="txtSircode" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                    </div>



                                                    <div class="col-md-3 pading5px asitCol2">
                                                        <asp:DropDownList ID="ddlSircode" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                                            TabIndex="5">
                                                        </asp:DropDownList>
                                                    </div>
                                                    
                                                    <div class="clearfix"></div>


                                                </div>--%>
                                        </fieldset>






                                        <div>
                                            <asp:GridView ID="gvKpiGrp" runat="server" AllowPaging="false" Width="200px"
                                                AutoGenerateColumns="False" ShowFooter="true" OnRowDeleting="gvKpiGrp_RowDeleting"
                                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                    Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="10px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:CommandField ShowDeleteButton="True" />

                                                    <asp:TemplateField HeaderText="Empid" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvEmpid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empid")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvEmpname" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"empname")) %>'
                                                                Width="140px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="KPI Grp">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvGrp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"kpigrp")) %>'
                                                                Width="80px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Company Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvComdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"comnam")) %>'
                                                                Width="140px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Wrkdept" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwrkdpt" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"wrkdpt")) %>'
                                                                Width="100px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dept Desc">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDptdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"wrkdptdesc")) %>'
                                                                Width="180px"
                                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatModInfo" runat="server" OnClick="lUpdatModInfo_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#F5F5F5" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--<asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-default" OnClick="lnkClose_Click">Close</asp:LinkButton>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>





            </div>
        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


