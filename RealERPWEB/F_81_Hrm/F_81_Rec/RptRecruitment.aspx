<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptRecruitment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RptRecruitment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName"
                                            Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to"
                                            Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" Text="Ok"></asp:LinkButton>
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </div>

                                </div>
                            </div>

                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Viewjobadvertise" runat="server">
                            <asp:GridView ID="gvappinfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False"
                                ShowFooter="True" Width="678px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Adv. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvadvno" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrefno" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdate" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advdat1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcompany" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "company")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptname" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpost" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qualification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqualification" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qfication")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requirement">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqualification" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "requir")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvjobsource" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobsource")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>




                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <AlternatingRowStyle BackColor="" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewShortlisting" runat="server">

                            <asp:Panel ID="Panel3" runat="server">
                                <div class="form-group">
                                    <div class="col-md-5  pading5px">
                                        <asp:Label ID="lblpreAdv" runat="server" CssClass="lblTxt lblName"
                                            Text="ADV List"></asp:Label>

                                        <asp:TextBox ID="txtSrchPre" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>

                                       <asp:LinkButton ID="ImgbtnFindAdv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindAdv_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlAdvList" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="250"
                                                OnSelectedIndexChanged="ddlAdvList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-5  pading5px">
                                        <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName"
                                            Text="Post List"></asp:Label>

                                        <asp:TextBox ID="txtPostSearch" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>

                                       <asp:LinkButton ID="ImgbtnFindPost" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindPost_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlPOSTList" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="250"
                                               >
                                            </asp:DropDownList>


                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn" ></asp:Label>
                                    </div>

                                </div>

                                <div class="clearfix"></div>
                             <%--   <table style="width: 900px">
                                    <tr>
                                        <td class="style78">
                                            <asp:Label ID="lblpreAdv" runat="server" CssClass="style15" Font-Bold="True"
                                                Font-Size="12px" Height="16px" Style="text-align: left" Text="ADV List:"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="style42">
                                            <asp:TextBox ID="txtSrchPre" runat="server" BorderStyle="None" Font-Bold="True"
                                                Font-Size="12px" TabIndex="11" Width="80px"></asp:TextBox>
                                        </td>
                                        <td align="right" class="style34">
                                            <asp:ImageButton ID="ImgbtnFindAdv" runat="server" Height="19px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindAdv_Click" TabIndex="12"
                                                Width="16px" />
                                        </td>
                                        <td class="style43">
                                            
                                        </td>
                                        <td class="style34">&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td class="style90">&nbsp;
                                        </td>
                                        <td class="style89">&nbsp;
                                        </td>
                                        <td class="style46">&nbsp;
                                        </td>
                                        <td class="style19"></td>
                                    </tr>
                                    <tr>
                                        <td class="style78">
                                            <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left; color: #FFFFFF;"
                                                Text="Post List:" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style42">
                                            <asp:TextBox ID="txtPostSearch" runat="server" BorderStyle="None"
                                                Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                        </td>
                                        <td align="right" class="style34">
                                            <asp:ImageButton ID="ImgbtnFindPost" runat="server" BorderStyle="None"
                                                Height="19px" ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPost_Click"
                                                TabIndex="16" Width="16px" />
                                        </td>
                                        <td class="style43">
                                            <asp:DropDownList ID="ddlPOSTList" runat="server" AutoPostBack="True"
                                                Font-Size="12px" Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                                TabIndex="17" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" class="style87">&nbsp;</td>
                                        <td class="style91">&nbsp;
                                        </td>
                                        <td class="style90">&nbsp;
                                        </td>
                                        <td class="style89">
                                            <asp:Label ID="lblmsg1" runat="server" __designer:wfdid="w4" BackColor="Red"
                                                Font-Bold="True" Font-Size="12px" ForeColor="White" Height="18px"
                                                Style="font-size: 12px; text-align: left"></asp:Label>
                                        </td>
                                        <td class="style46">&nbsp;
                                        </td>
                                        <td class="style19">&nbsp;
                                        </td>
                                    </tr>
                                </table>--%>
                            </asp:Panel>
                            <div class="clearfix"></div>

                            <div class=" table-responsive">
                            <asp:GridView ID="gvSListInfo" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvSListInfo_PageIndexChanging"
                                PageSize="15" ShowFooter="True" Width="16px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPostCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl Number" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvissue" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "listisu")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col1">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol1" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col1").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol2" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col2").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol3" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col3").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol4" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col4").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol5" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col5").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol6" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col6").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol7" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col7").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol8" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col8").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol9" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col9").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol10" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col10").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol11" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col11").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol12" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col12").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col13">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol13" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col13").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Col14">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol14" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col14").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col15">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol15" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col15").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col16">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol16" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col16").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Col17">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCol13" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "col17").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   



                                    <asp:TemplateField HeaderText="Total Mark">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltomark" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tomark")).ToString("#,##0.00;(#,##0.00);") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            </div>
                            <asp:Label ID="lblInterViewer" runat="server" CssClass="btn btn-success primaryBtn" Text="Interviewer Information"
                                Width="300px" Visible="False"></asp:Label>

                            <div class="clearfix"></div>
                            <asp:GridView ID="gvIntInfo" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvIntInfo_PageIndexChanging"
                                PageSize="15" ShowFooter="True" Width="16px">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Postcode Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPostCod0" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Int Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIntCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name of Interviewer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIntDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intdesc"))   %>'
                                                Width="150px">
                                                            
                                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Interview">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIntDat" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRem" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
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




        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

