<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEntry.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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


            <div class="card card-fluid container-data">
                <div class="card-header mb-0 pb-0">
                    <div class="row mb-0 pb-0">

                        <div class="col-md-6">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Employee List</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpName" data-placeholder="Choose Employee.." ClientIDMode="Static" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Information</button>
                                </div>
                                <asp:DropDownList ID="ddlInformation" data-placeholder="Choose Information.." ClientIDMode="Static" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlInformation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblLastCardNo" runat="server" Visible="false" CssClass=" btn btn-info primaryBtn btn-sm"></asp:Label>

                        </div>
                        <div class="col-md-2" runat="server" visible="false">
                            <a href="#" class="btn btn-info btn-xs" onclick="history.go(-1)">Back</a>
                            <asp:HyperLink runat="server" CssClass="btn  btn-primary btn-xs" NavigateUrl="HREmpEntry?Type=Aggrement" Target="_blank">Next Aggrement</asp:HyperLink>
                        </div>
                    </div>

                    <div class="row mb-0 pb-0">
                        <div class="col-md-3">
                            <asp:HyperLink ID="addOcupation" Visible="False" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_82_App/HRCodeBook.aspx" CssClass="btn btn-success btn-sm" Style="padding: 0 10px">Add Occupation</asp:HyperLink>
                        </div>

                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvPersonalInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="2px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Bold="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server"  CssClass="btn btn-danger  btn-xs" OnClick="lUpdatPerInfo_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="400px"></asp:TextBox>
                                                <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="400px"></asp:TextBox>

                                                <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                    enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal"></cc1:calendarextender>
                                                <asp:Panel ID="Panegrd" runat="server">

                                                    <div class="form-group">
                                                        <div class="col-md-12 pading5px">
                                                            <asp:TextBox ID="txtgrdEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                            <asp:LinkButton ID="ibtngrdEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtngrdEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                            <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>



                                                        </div>
                                                    </div>


                                                </asp:Panel>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bangla">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvValBn" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>' autocomplete="off"
                                                    Width="250px"></asp:TextBox>

                                            </ItemTemplate>
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
                            <asp:View ID="ViewDegree" runat="server">
                            </asp:View>
                            <asp:View ID="ViewCompany" runat="server">
                            </asp:View>
                            <asp:View ID="ViewAssociation" runat="server">
                            </asp:View>
                            <asp:View ID="ViewRef" runat="server">
                            </asp:View>
                            <asp:View ID="ViewJobRespo" runat="server">
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                            </asp:View>
                            <asp:View ID="View3" runat="server">
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
