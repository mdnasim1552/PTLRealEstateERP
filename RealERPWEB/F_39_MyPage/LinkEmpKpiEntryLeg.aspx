<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkEmpKpiEntryLeg.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkEmpKpiEntryLeg" %>

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
  

    <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>

                                <asp:TextBox ID="txtFrom" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFrom">
                                </cc1:CalendarExtender>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                

                            </div>


                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                </div>

                            </div>
                            
                            <div class="col-md-2  pull-right">                                
                                <a href="javascript:window.close();" class="btn btn-primary primaryBtn">close</a>
                               

                            </div>

                           
                        </div>

                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>
                                <div class="colMdbtn">
                                    <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                    <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                </div>
                            </div>



                            <div class="col-md-3 pading5px asitCol3">
                                <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="ddlPage235 inputTxt"
                                    TabIndex="5">
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                            </div>
                            <div class="col-md-1 pading5px">
                                <asp:Label ID="lbltransno" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>

                                 <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>
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
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="form-control inputTxt"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="235px"></asp:TextBox>
                                                            <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="form-control inputTxt"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="235px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal">
                                                            </cc1:CalendarExtender>

                                                            <asp:Panel ID="PnlProject" runat="server">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:TextBox ID="txtProSearch" runat="server" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>


                                                                    <div class="colMdbtn">
                                                                        <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                    </div>


                                                                    <div class="ddlListPart">
                                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="smDropDownGrid inputTxt"
                                                                         
                                                                            TabIndex="12">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </asp:Panel>

                                                            <asp:Panel ID="PnlUnit" runat="server">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:TextBox ID="txtUnitSearch" runat="server" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>


                                                                    <div class="colMdbtn">
                                                                        <asp:LinkButton ID="imgSearchUnit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                    </div>


                                                                    <div class="ddlListPart">
                                                                        <asp:DropDownList ID="ddlUnit" runat="server"  CssClass="smDropDownGrid inputTxt"
                                                                           
                                                                            TabIndex="12">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

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
                                        CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px" >
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
                                          
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Case Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcasename" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Activities">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactivities" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wsirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Court Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcourtname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                       <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                           
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussionpdisc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                <div class="table-responsive">

                </div>


              





            </div>
        </div>
    </div>

   <script>
       function Exit() {
           var x = confirm('Are You sure want to exit:');
           if (x) window.close();
       }
   </script>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

