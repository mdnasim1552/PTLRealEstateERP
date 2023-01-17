<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MonthlySalesBudget.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MonthlySalesBudget" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="container moduleItemWrpper">
                <div class="contentPart">
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
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="lCurAppdate" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>
                                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <asp:GridView ID="gvSalbgd" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="531px" OnRowDataBound="gvSalbgd_OnRowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbTotal_Click"> Total </asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate"
                                                CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" id="empid"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' Visible="False" ></asp:Label>
                                           
                                             
                                            
                                            
                                            
                                             <asp:HyperLink ID="lblgvDepartment" runat="server"  Target="_blank"
                                               
                                                Style="text-align: Left; background-color: Transparent; color:black;"
                                                
                                               
                                                  Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                       
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "") 
                                                                         
                                                                    %>'
                                                 
                                                 
                                                  Width="200px"></asp:HyperLink>




                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSaleTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsalamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collection">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCollTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcollamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Today's Sale">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoSaleTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtogvtosalamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Today's Collection">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoCollTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtocollamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="ViewYearly" runat="server">


                            <asp:Panel ID="Panel2" runat="server">
                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">

                                            <div class="form-group">

                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Year"></asp:Label>
                                                    <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                                    </asp:DropDownList>
                                                    <asp:LinkButton ID="lbtnYearbgd" runat="server" OnClick="lbtnYearbgd_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="lblmsg02" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>

                            </asp:Panel>

                            <asp:GridView ID="gvySalbgd" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="531px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbYearbgdTotal" runat="server" OnClick="lbYearbgdTotal_Click" CssClass="btn btn-primary primaryBtn"> Total </asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnYBgdUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnYBgdUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDepartmentyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSaleTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsalamtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collection">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCollTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcollamtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Purchase">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPurTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpuramtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puramt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Today's Sale">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoSaleTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtogvtosalamtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Today's Collection">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoCollTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtocollamtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Today's Purchase">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoPurTotalyb" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtopuramtyb" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpuramt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />


                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="ViewDailyReport" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Day"></asp:Label>
                                                <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                                <asp:LinkButton ID="lbtnDailyRpt" runat="server" OnClick="lbtnDailyRpt_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <asp:GridView ID="gvDailyEntry" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="531px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoidy0" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpDailyEntry" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  OnClick="lbtnUpDailyEntry_Click"
                                                            Style="text-decoration: none;">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDeptNameDaily" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: Left; background-color: Transparent"
                                                            Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))+"<b/>" %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Activities">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvactivities" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: Left; background-color: Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Unit">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: Left; background-color: Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Particulars">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpertuculars" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prticlars")).ToString("#,##0;-#,##0; ")%>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                            
                        </asp:View>


                        <asp:View ID="ViewMonTypeWise" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>
                                                <asp:DropDownList ID="ddlmonthtypeWise" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="lblMontypeWise" runat="server" OnClick="lblMontypeWise_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label3" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <asp:GridView ID="gvsbgdTypeWise" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="531px" OnRowCreated="gvsbgdTypeWise_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidbgd" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbTotalbgd" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbTotalbgd_Click"> Total </asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnbgdFinalUpdate"
                                                CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lbtnbgdFinalUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" id="empid"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' Visible="False" ></asp:Label>
                                           
                                             
                                            
                                            
                                            
                                             <asp:HyperLink ID="lblgvDepartmentbgd" runat="server"  Target="_blank"
                                               
                                                Style="text-align: Left; background-color: Transparent; color:black;"
                                                
                                               
                                                  Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                       
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "") 
                                                                         
                                                                    %>'
                                                 
                                                 
                                                  Width="200px"></asp:HyperLink>




                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apt">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFaptqty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvaptqty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Shop">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFShopqty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvshopqty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shopqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Apt">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAptCollTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAptcollamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shop">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFShopCollTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvShopcollamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shopamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                  <%--  <asp:TemplateField HeaderText=" Today's Collection">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoCollTotal" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtocollamt" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>


                    </asp:MultiView>
                </div>
            </div>


            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

