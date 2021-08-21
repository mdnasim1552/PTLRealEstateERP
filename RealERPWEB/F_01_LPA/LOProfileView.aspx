<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LOProfileView.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LOProfileView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .asitbtn {
            padding-left: 10px !important;
            margin-left: 5px !important;
        }

        .smGroup {
            padding: 2px 12px;
        }
    </style>

    <script>
        function AddButton(id) {
            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }

        function AddButtonsub(id) {
            $(".hiddensub" + id).css("display", "inline");

        }
        function HiddenButtonsub(id) {
            $(".hiddensub" + id).css("display", "none");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppnlclint" runat="server">
        <ContentTemplate>
            <script>
                function openModal() {
                    //    $('#myModal').modal('show');
                    $('#myModal').modal('toggle');
                }
                function CloseModal() {

                    $('#myModal').modal('hide');
                }


                function opencommModal() {
                    //    $('#myModal').modal('show');
                    $('#contact').modal('toggle');
                }

                function ClosecommModal() {

                    $('#contact').modal('hide');
                }


            </script>

            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">

                        <div style="padding-top: 50px;"></div>
                        <div class="col-lg-3 col-md-3 hidden-sm hidden-xs">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="media">
                                        <div align="center">
                                            <img class="img-circle" src="https://lut.im/7JCpw12uUT/mY0Mb78SvSIcjvkf.png" width="100px" height="100px">
                                        </div>
                                        <div class="media-body">
                                            <hr>
                                            <h5><strong>Bio</strong></h5>
                                            <label id="lblspous" runat="server"></label>
                                            <span id="ClintSpousNam" runat="server"></span>
                                            <br />
                                            <label id="lblFather" runat="server"></label>
                                            <span id="ClientFather" runat="server"></span>
                                            <br />
                                            <label id="lblMother" runat="server"></label>
                                            <span id="ClientMother" runat="server"></span>
                                            <br />
                                            <label id="lblproffession" runat="server"></label>
                                            <span id="ClientProffession" runat="server"></span>
                                            <br />
                                            <label id="lblNatid" runat="server"></label>
                                            <span id="ClientNaid" runat="server"></span>
                                            <br />
                                            <label id="lblpassport" runat="server"></label>
                                            <span id="Clientpassport" runat="server"></span>
                                            <br />
                                            <label id="lblTin" runat="server"></label>
                                            <span id="ClientTin" runat="server"></span>
                                            <br />

                                            <h5><strong id="lblloc" runat="server">Location</strong></h5>
                                            <p id="Clientloc" runat="server">East Side</p>

                                            <%--   <h5><strong >Gender</strong></h5>
                                            <p>Male</p>
                                            --%>
                                            <h5><strong id="lblbirth" runat="server">Birthday</strong></h5>
                                            <p id="clintbirth" runat="server">10-Aug-1989</p>
                                            <h5><strong id="lblmarrage" runat="server">Marrage Day</strong></h5>
                                            <p id="ClientMarrage" runat="server"></p>
                                            <h5><strong id="lblPresent" runat="server">Present Address</strong></h5>
                                            <p id="ClientpAdd" runat="server">241, South Peererbag, 60 Feet Road, Aamtola, Mirpur, Dhaka-1216</p>
                                            <h5><strong id="lblpermanent" runat="server">Permanent Address</strong></h5>
                                            <p id="ClientperAdd" runat="server">Plot A, 86, Gulshan North Ave, Gulsan 1, Dhaka-1230 </p>
                                            <h5><strong id="lblofficialadd" runat="server"></strong></h5>
                                            <p id="ClientoffAdd" runat="server"></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="perosnalinfo pull-left">

                                        <span id="LandInfo" runat="server">:</span>

                                        <strong id="LandName" runat="server"></strong>


                                        <br />
                                        <span id="ClientName" runat="server">:</span>

                                        <strong id="ClientName1" runat="server"></strong>


                                        <br />
                                        <span id="lblEmail" runat="server"></span>
                                        <span><strong id="ClintMail" runat="server"></strong></span>
                                        <br>


                                        <span id="lblphn" runat="server"></span>
                                        <strong id="ClintPhn" runat="server"></strong>

                                        <br>
                                        <span id="lblTphon" runat="server" style="display:none;"></span>
                                        <strong id="ClientTphn" runat="server" style="display:none;"></strong>
                                        <br />
                                    </div>
                                    <div class="allbtn pull-right">

                                        <asp:LinkButton ID="lnkSndMail" CssClass="btn btn-success btn-sm" runat="server" OnClick="lnkSndMail_Click" Text="Send Email"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkNdisscuss" CssClass="btn btn-primary btn-sm" OnClick="lnkNdisscuss_Click" runat="server" Text="New Discuss"></asp:LinkButton>
                                        <%--<button class="btn btn-success "  type="button" id="btnSndMail" >Send Email </button>--%>
                                        <%--<button class="btn btn-success asitbtn" type="button" id="btnDiscuss">New Discuss
                                         </button>--%>
                                    </div>


                                </div>
                            </div>
                            <hr>
                            <!-- Simple post content example. -->
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:GridView ID="gvclient" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered  grvContentarea" OnRowDataBound="gvclient_RowDataBound">
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


                                            <asp:TemplateField HeaderText="Meeting </br>Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                        Width="110px"></asp:Label>

                                                    <asp:Label ID="lblgvDate" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvkpigrp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpigrpdesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>

                                                    <asp:Panel ID="pnldis" runat="server" ClientIDMode="Static">

                                                        <asp:Label ID="lgvDiscussion0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                            Width="150px">                                             


                                                        </asp:Label>
                                                        <asp:LinkButton ID="lnkAdddis" ClientIDMode="Static" Width="10" ToolTip="Comments" runat="server" OnClick="lnkAdddis_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvdisgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "disgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Participants">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpartcilist" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Font-Size="9px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next </br>Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnfollowup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Next </br>Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                        <asp:Label ID="lgvndissub" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                            Width="100px"></asp:Label>
                                                        <asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>

                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content" style="background: #fff;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Send Email Information</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-md-3">Client Email</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <span class="input-group-addon smGroup"><span class="glyphicon glyphicon-envelope"></span>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="ClienEmail" CssClass="form-control"></asp:TextBox>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Subject</label>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="subject" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Message</label>
                                            <div class="col-md-9">
                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="msgbody" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:LinkButton ID="SubmitMsg" runat="server" OnClick="SubmitMsg_Click" OnClientClick="CloseModal()" Class="btn btn-primary"><span class="glyphicon glyphicon-send"></span> Send Email</asp:LinkButton>

                                </div>
                            </div>

                        </div>
                    </div>



                    <div class="modal fade right" id="contact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="false">
                            <div class="modal-dialog  modal-lg  modal-side modal-bottom-right modal-notify modal-info" role="document">
                                <!--Content-->
                                <div class="modal-content">
                                    <!--Header-->
                                    <div class="modal-header">
                                        <p class="heading">
                                            <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span></h4>
                                        </p>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>
                                    </div>

                                    <!--Body-->
                                    <div class="modal-body">

                                        <div class="row">

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label  id="lbldsi" runat="server" class="control-label lblmargin-top9px"></label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lbldiscussion" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px" style="background:#DFF0D8"></asp:Label>


                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblcomm" class="control-label lblmargin-top9px">Comments:</label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtComm" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px"></asp:TextBox>


                                                </div>
                                            </div>


                                            <asp:Panel ID="pnld" runat="server" Visible="false">

                                            <asp:Label ID="lblEmpid" runat="server"></asp:Label>
                                            <asp:Label ID="lblclient" runat="server"></asp:Label>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                           

                                            </asp:Panel>


                                        </div>
                                    </div>

                                    <!--Footer-->
                                    <div class="modal-footer">


                                        <asp:LinkButton ID="lUpdatInfo" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="ClosecommModal();" OnClick="lUpdatInfo_Click">Save</asp:LinkButton>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>

                                    </div>
                                </div>
                                <!--/.Content-->
                            </div>
                        </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

