<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="linkMktEmpKpiEntry.aspx.cs" Inherits="RealERPWEB.F_21_MKT.linkMktEmpKpiEntry" %>

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

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            var gvclient = $('#<%=this.gvclient.ClientID %>');

            gvclient.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 10
            });





            $('.chzn-select').chosen({ search_contains: true });
        }

        function SearchSalesTeam() {

            var srchteam = "%" + $('#<%=this.txtSrchSalesTeam.ClientID%>').val() + "%";
            var userid = $('#<%=this.lbluseid.ClientID%>').text().trim();
            var objsalesteam = new RealERPScript();
            var lst = objsalesteam.GetEmpCode(srchteam, userid);
            var ddlemployee = $('#<%=this.ddlEmpid.ClientID %>');
            ddlemployee.children('option').remove();
            $.each(lst, function (index, lst) {
                ddlemployee.append('<option value="' + lst.empid + '">' + lst.empname + '</option>');
            });
        }
    </script>


         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-2 pading5px">
                                <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>

                                <asp:TextBox ID="txtFrom" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFrom"></cc1:CalendarExtender>

                            </div>
                            <div class="col-md-3 pading5px">
                                <asp:Label ID="lblSalesTeam" runat="server" CssClass="smLbl_to" Text="Employee Name:"></asp:Label>



                                <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" Visible="false" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>



                                <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="chzn-select inputTxt" Width="180px"
                                    TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3 pading5px">




                                <asp:Label ID="lblClientName" runat="server" CssClass="smLbl_to" Text="Client Name"></asp:Label>
                                <asp:TextBox ID="txtClient" runat="server" TabIndex="3" Visible="false" CssClass=" inputtextbox"></asp:TextBox>

                                <asp:LinkButton ID="btnClient" runat="server" OnClick="btnClient_Click" Visible="false" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt" Width="180px"
                                    TabIndex="5">
                                </asp:DropDownList>
                            </div>


                            <asp:RadioButtonList ID="rbtnlist" runat="server" RepeatDirection="Horizontal"  
                                CssClass="btn btn-primary  primarygrdBtn">
                                <asp:ListItem Value="810100601003">&nbsp;&nbsp;Offer &nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="810100601004" >&nbsp;&nbsp;Visit &nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Selected="True" Value="810100601005">&nbsp;&nbsp;Call &nbsp;&nbsp;</asp:ListItem>

                            </asp:RadioButtonList>
                             <div class="col-md-2 pading5px ">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>

                                <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn-sm btn" Visible="false"></asp:Label>

                                <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName " Style="display: none;"></asp:Label>
                                <a href="javascript:window.close();" class="btn btn-primary primaryBtn margin5px">close</a>

                            </div>
                        </div>
                    </div>
                </fieldset>
                <asp:MultiView ID="MultiView1" runat="server">

                    <asp:View ID="viewEmp" runat="server">

                        <asp:GridView ID="gvInfo" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" PageSize="25" ShowFooter="true" Width="600px"
                            CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcGrp" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="Modalupdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtgvVal" runat="server" BorderWidth="0" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="320px"></asp:TextBox>

                                        <asp:TextBox ID="txtgvdVal" runat="server" BorderWidth="0" BackColor="Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="320px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy HH:mm" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                      



                                        <asp:Panel ID="PnlProject" runat="server">

                                            <asp:TextBox ID="txtProSearch" runat="server" Visible="false" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>



                                            <asp:LinkButton ID="imgSearchProject" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                                                TabIndex="12">
                                            </asp:DropDownList>


                                        </asp:Panel>

                                        <asp:Panel ID="PnlUnit" runat="server">

                                            <asp:TextBox ID="txtUnitSearch" runat="server" Visible="false" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>



                                            <asp:LinkButton ID="imgSearchUnit" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="chzn-select inputTxt form-control"
                                                OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"
                                                TabIndex="12">
                                            </asp:DropDownList>

                                        </asp:Panel>



                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                        <div class=" col-md-12 pading5px">
                            <asp:Label ID="lblHeaderPredis" runat="server" CssClass="smLbl_to pading5px" Text=" Previous Discussion" Visible="false"></asp:Label>

                        </div>
                        <div class="clearfix"></div>

                        <div class="table-responsive">
                            <asp:GridView ID="gvclient" runat="server"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Phone No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meeting Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpronamech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitsizech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacpricech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacotherch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvactoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvofratech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvofparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other">
                                        <ItemTemplate>
                                            <asp:Label ID="lgofotherch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asking Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvoftoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Diff. In %">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdiffinper" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destination">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcallovipurposech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Call/Visit time">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcallovtimech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calovtime")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discussion">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDiscussion0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Appointment">
                                        <ItemTemplate>
                                            <asp:Label ID="nappdat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                    </asp:View>

                </asp:MultiView>
             
            </div>
        </div>
    </div>

    <script>
        function Exit() {
            var x = confirm('Are You sure want to exit:');
            if (x) window.close();
        }
    </script>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

