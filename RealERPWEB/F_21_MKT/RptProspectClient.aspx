<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProspectClient.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptProspectClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="card card-fluid">
        <div class="card-body" style="min-height: 600px;">
    <div class="row" style="padding-top: 15px">

        <div class="col-md-1">
            <div class="form-group">
                <asp:DropDownList ID="ddlSmsMail" runat="server" OnSelectedIndexChanged="ddlSmsMail_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                    <asp:ListItem Value="01">SMS</asp:ListItem>
                    <asp:ListItem Value="02">Mail</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlSMSMAILTEMP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSMSMAILTEMP_SelectedIndexChanged" CssClass="form-control">
            </asp:DropDownList>

        </div>

        <div class="col-md-6">
            <asp:Label ID="lblTemMSg" runat="server" Text="This is Test for Template "></asp:Label>
        </div>

        <div class="col-md-1">
            <asp:LinkButton ID="lnkSend" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkSend_Click" Text="Send"></asp:LinkButton>

        </div>

    </div>


    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
        <RowStyle />
        <Columns>

            <asp:TemplateField HeaderText="Sl">
                <ItemTemplate>
                    <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                        Style="text-align: right"
                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>






            <asp:TemplateField HeaderText="Code" Visible="false">
                <ItemTemplate>

                    <asp:Label ID="lblusercode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usercode")) %>'></asp:Label>
                    <asp:Label ID="lsircode" runat="server"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                    <asp:Label ID="ldesig" runat="server" Width="40px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="P-ID">
                <ItemTemplate>
                    <asp:Label ID="lsircode1" runat="server" Width="40px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Generated">
                <ItemTemplate>
                    <asp:Label ID="lblgenerated" runat="server" Width="60px" Font-Size="10px"
                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>




            <asp:TemplateField HeaderText="Client Details">
                <ItemTemplate>
                    <asp:Label ID="ldesc" runat="server" Width="100px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Associate">
                <ItemTemplate>
                    <asp:Label ID="lassoc" runat="server" Width="120px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                </ItemTemplate>


            </asp:TemplateField>

            <asp:TemplateField HeaderText="Team Head">
                <ItemTemplate>
                    <asp:Label ID="lblbusername" runat="server" Width="120px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                </ItemTemplate>


            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lbllstatus" runat="server" Width="50px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="llTyp" runat="server" Width="40px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lappdat" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
            <%--  <asp:TemplateField HeaderText="Preferred Location" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lprefdesc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lLSrc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>





            <asp:TemplateField HeaderText="Mobile">
                <ItemTemplate>
                    <asp:Label ID="lblgvphone" runat="server" Width="80px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblgvemail" runat="server" Width="120px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Occupation">
                <ItemTemplate>
                    <asp:Label ID="lblgvoccupation" runat="server" Width="80px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Residence" Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="lblgvcaddress" runat="server" Width="120px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Interested Project">
                <ItemTemplate>
                    <asp:Label ID="lblgvpactdesc" runat="server" Width="90px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Source">
                <ItemTemplate>
                    <asp:Label ID="lblgvLSrc" runat="server" Width="70px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            

            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkdiss" ClientIDMode="Static" Width="20px" ToolTip="Add Discussion" runat="server">
                                                    <img src="../Image/meeting.svg" width="20" height="20" />
                    </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <%-- <asp:CheckBox ID="chkassign" runat="server"
                                        Width="20px" />--%>
                    <asp:CheckBox ID="chkstatus" runat="server"
                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smstatus"))=="1" %>'
                        Width="20px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Feedback">
                <ItemTemplate>
                    <asp:Label ID="lblgvfeedback" runat="server" Width="120px" Font-Size="10px"
                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
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
            </div>
</asp:Content>


