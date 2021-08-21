<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptClientDis.aspx.cs" Inherits="RealERPWEB.F_62_Mis.RptClientDis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    


  

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblNameSm" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>

                                         <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to" Text="From:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" Text="To:" TabIndex="1" CssClass=" smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="2"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                        
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

                            </div>
                        </fieldset>

                        <div class="table-responsive">
                            <asp:GridView ID="gvClientDis" runat="server" AllowPaging="True" Width="200px"
                                AutoGenerateColumns="False" PageSize="10" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvClientDis_PageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                 <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgClNameall" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prosdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="150px">
                                                 
                                                 
                                                 
                                                 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdatall" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address &amp; Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhoneall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpronameopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunitnameopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacpriceopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacparkingopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacotheropall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactoamtopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Offered Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvofrateopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvofparkingopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other">
                                            <ItemTemplate>
                                                <asp:Label ID="lgofotheropall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoftoamtopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Diff in %">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffinperoffall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Destination">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcallovipurposeopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call/Visit time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcallovtimeopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calovtime")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDiscussionall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdatall" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
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

                        </div>



                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

