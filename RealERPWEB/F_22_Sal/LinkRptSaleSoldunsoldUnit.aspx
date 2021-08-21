<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptSaleSoldunsoldUnit.aspx.cs" Inherits="RealERPWEB.F_22_Sal.LinkRptSaleSoldunsoldUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                             <div class="col-md-3 asitCol2  pading5px">

                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                    Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox inputTxt" Width="80px"
                                    BorderStyle="None"></asp:TextBox>
                                

                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            
                            <div class="col-md-3 pading5px asitCol3  pull-left">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass=" form-control inputTxt" Font-Bold="True"
                                    OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-7 pading5px">


                                <asp:Label ID="Label15" runat="server" CssClass=" smLbl_to"
                                    Text="Date:"></asp:Label>


                                <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox " Width="80px"
                                    BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>


                                <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to"
                                    Text="To:"></asp:Label>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox inputTxt" AutoPostBack="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                           

                                <asp:Label ID="lblSalesTeam" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                    Text="Sales Team:"></asp:Label>

                                <asp:DropDownList ID="ddlSalesTeam" CssClass="ddlPage" runat="server" AutoPostBack="True"
                                    Font-Bold="True" Width="80px"
                                    Style="text-transform: capitalize"
                                    OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Label ID="lblGroup" runat="server" CssClass=" smLbl_to" Font-Bold="True"
                                    Text="Group:"></asp:Label>

                                <asp:DropDownList ID="ddlRptGroup" CssClass="ddlPage" runat="server" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChanged"
                                    Style="text-transform: capitalize" Width="60px">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                               
                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                    Text="Size:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="ddlPage" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                    Width="50px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary  okBtn " OnClick="lbtnOk_Click"
                                    Font-Underline="False">Ok</asp:LinkButton>
                           
                            </div>
                         

                        </div>


                  
                      
                    </div>
                </fieldset>
                <div class=" table-responsive">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="SoldUnsold" runat="server">


                            <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                OnPageIndexChanging="gvSpayment_PageIndexChanging" OnRowDataBound="gvSpayment_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCuName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">

                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Budgeted Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="35px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>







                                    <asp:TemplateField HeaderText="Sold Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="35px" Style="text-align: right; color: red;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right; color: red;" Width="35px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Size(Sold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsusize" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "susize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTsusize" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate(Sold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sold Amt">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lgvSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>
                                          
                                            <asp:HyperLink ID="HplgvAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sold Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSchdate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                Width="65px" Style="text-align: left"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Unsold Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUsqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="35px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFUsqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Size(Unsold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvusize" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTusize" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Rate(Unsold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvurate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unsold Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUsAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDisAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDisAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDisPer" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ParkingStatus" runat="server">

                            <asp:GridView ID="gvparking" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                OnPageIndexChanging="gvparking_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvtotal" Text="Total" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvftoqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sold">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sold")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvPSAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unsold">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPUsAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsold")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvPUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Parking Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTqty" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bstat")) %>'
                                                Width="70px" Style="text-align: left"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="120px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                        </asp:View>
                        <asp:View ID="RptDayWSale" runat="server">


                            <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                OnPageIndexChanging="gvDayWSale_PageIndexChanging"
                                OnRowDataBound="gvDayWSale_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDPactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDcuname" runat="server"
                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">

                                        <ItemTemplate>
                                            <asp:Label ID="lgvDResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">

                                        <ItemTemplate>
                                            <asp:Label ID="lgUnit" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                Width="35px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUSize" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUpsft" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Team">

                                        <ItemTemplate>
                                            <asp:Label ID="lgDCper" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                Width="120px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDTAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Amt">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="HplgvSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>


                                           <%-- <asp:Label ID="lgvDSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sold Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDSchdate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                Width="65px" Style="text-align: left"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDDisAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgDvDisPer" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



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
        </div>
    </div>


</asp:Content>



